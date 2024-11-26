<?php

require_once("IDatabaseRepository.php");
require_once("FileRepository.php");
require_once("TagRepository.php");
require_once("FileTagRepository.php");
require_once("../Models/File.php");
require_once("../Models/Tag.php");
require_once("../Models/FileTag.php");

use File\File;
use Tag\Tag;
use FileTag\FileTag;

class DatabaseRepository implements IDatabaseRepository 
{
    private $pdo;
    private $fileRepo;
    private $tagRepo;
    private $fileTagRepo;

    public function __construct($fileRepo, $tagRepo, $fileTagRepo) {
        $this->pdo = getConnection(); 
        $this->fileRepo = $fileRepo;
        $this->tagRepo = $tagRepo;
        $this->fileTagRepo = $fileTagRepo;
    }

    public function exportDatabase()
    {
        try {
            $files = $this->fileRepo->getAllFiles(); 
            $tags = $this->tagRepo->getAllTags();    
            $fileTags = $this->fileTagRepo->getAllFileTags();
    
            if ($files === false || $tags === false || $fileTags === false) {
                throw new Exception("Failed to retrieve data from the database.");
            }
    
            $filesDir = 'C:\laragon\www\ProjetoFinal\Files';
    
            if (!is_dir($filesDir)) {
                throw new Exception("The 'Files' directory does not exist at path: " . $filesDir);
            }
            
            error_log("Files directory: " . $filesDir);
    
            $exportData = [
                'files' => [],
                'tags' => [],
                'file_tags' => []
            ];
    
            foreach ($files as $file) {
                $exportData['files'][] = [
                    'id' => $file->getId(),
                    'name' => $file->getName(),
                    'path' => $file->getPath(),
                    'type' => $file->getType()
                ];
            }
    
            foreach ($tags as $tag) {
                $exportData['tags'][] = [
                    'id' => $tag->getId(),
                    'name' => $tag->getName(),
                    'description' => $tag->getDescription()
                ];
            }
    
            foreach ($fileTags as $fileTag) {
                $exportData['file_tags'][] = [
                    'file_id' => $fileTag['fileId'],
                    'tag_id' => $fileTag['tagId']
                ];
            }
    
            $zipFilePath = $filesDir . DIRECTORY_SEPARATOR . 'export_backup.zip';
            
            error_log("ZIP file will be created at: " . $zipFilePath);
    
            $zip = new ZipArchive();
            if ($zip->open($zipFilePath, ZipArchive::CREATE) !== TRUE) {
                throw new Exception("Failed to create ZIP archive.");
            }
    
            $this->addFilesToZip($filesDir, $zip);
    
            $jsonData = json_encode($exportData, JSON_PRETTY_PRINT);
            $zip->addFromString('database_metadata.json', $jsonData);
    
            $zip->close();
    
            if (!file_exists($zipFilePath)) {
                throw new Exception("ZIP file was not created correctly.");
            }
    
            header('Content-Type: application/zip');
            header('Content-Disposition: attachment; filename="export_backup.zip"');
            header('Content-Length: ' . filesize($zipFilePath));
            readfile($zipFilePath);
    
            unlink($zipFilePath);
    
            return true;
    
        } catch (Exception $e) {
            error_log("Error during export: " . $e->getMessage());
            unlink('C:\laragon\www\ProjetoFinal\Files\export_backup.zip');
            return false; 
        }
    }
    
    private function addFilesToZip($dir, $zip)
    {
        // Add all files in the 'Files' directory to the ZIP
        $files = scandir($dir);
    
        // Log the files being scanned
        error_log("Scanning files in directory: " . $dir);
    
        foreach ($files as $file) {
            if ($file == '.' || $file == '..') {
                continue;
            }
    
            $fullPath = $dir . DIRECTORY_SEPARATOR . $file;
    
            // Log each file being added
            error_log("Adding file to ZIP: " . $fullPath);
    
            // Ensure we're only adding files, not directories
            if (is_file($fullPath)) {
                $zip->addFile($fullPath, 'Files/' . $file); // Add the file to the root of the ZIP
            }
        }
    }

    public function importDatabase($zipFile)
    {
        try {
            $zip = new ZipArchive();
            if ($zip->open($zipFile) !== TRUE) {
                throw new Exception("Failed to open ZIP file.");
            }

            $zip->extractTo('C:\laragon\www\ProjetoFinal');
            unlink('C:\laragon\www\ProjetoFinal\database_metadata.json');
    
            $jsonFile = 'database_metadata.json';
            if ($zip->locateName($jsonFile) === false) {
                throw new Exception("Database metadata file not found in the ZIP archive.");
            }
    
            $jsonContent = $zip->getFromName($jsonFile);
            $data = json_decode($jsonContent, true);
            
            if (!$data) {
                throw new Exception("Failed to decode database metadata JSON.");
            }
    
            $filesDir = 'C:/laragon/www/ProjetoFinal/Files'; 
            
            if (!is_dir($filesDir)) {
                mkdir($filesDir, 0777, true); 
            }
            
            $oldToNewFileIds = [];
            $oldToNewTagIds = [];
    
            $this->importFiles($data['files'], $oldToNewFileIds);
    
            $this->importTags($data['tags'], $oldToNewTagIds);
    
            $this->importFileTags($data['file_tags'], $oldToNewFileIds, $oldToNewTagIds);
    
            $zip->close();
    
            return true;
    
        } catch (Exception $e) {
            error_log("Error during import: " . $e->getMessage());
            return false; 
        }
    }

    private function importFiles($filesData, &$oldToNewFileIds)
    {
        try {
            $fileRepo = new FileRepository();  

            $filesDir = 'C:/laragon/www/ProjetoFinal/Files'; 
    
            foreach ($filesData as $fileData) {
                
                $file = new File();
                $file->setName($fileData['name']);  
                $file->setPath($fileData['path']);  
                $file->setType($fileData['type']);  
                error_log('add file');
                
                $newFile = $fileRepo->addFile($file);
                error_log('get id');
                
                $oldToNewFileIds[$fileData['id']] = $newFile->getId();
                error_log('baename');
                $newFileName = basename($newFile->getPath());
                error_log('DatabaseRepository: importFiles()'.$fileData['path']. $newFileName);
            }
        } catch (Exception $e) {
            error_log("Error importing files: " . $e->getMessage());
        }
    }

    private function importTags($tagsData, &$oldToNewTagIds)
    {
        try {
            $tagRepo = new TagRepository();  
    
            foreach ($tagsData as $tagData) {

                $tag = new Tag();
                $tag->setName($tagData['name']);
                $tag->setDescription($tagData['description']);
                
                $newTag = $tagRepo->addTag($tag);  
                
                $oldToNewTagIds[$tagData['id']] = $newTag->getId();
            }
        } catch (Exception $e) {
            error_log("Error importing tags: " . $e->getMessage());
        }
    }
    

    private function importFileTags($fileTagsData, $oldToNewFileIds, $oldToNewTagIds)
    {
        try {
            $fileTagRepo = new FileTagRepository();
    
            foreach ($fileTagsData as $fileTagData) {
                $oldFileId = $fileTagData['file_id'];
                $oldTagId = $fileTagData['tag_id'];
    
                $newFileId = $oldToNewFileIds[$oldFileId] ?? null;
                $newTagId = $oldToNewTagIds[$oldTagId] ?? null;
    
                if ($newFileId && $newTagId) {
                    
                    $fileTag = new FileTag();
                    $fileTag->setFileId($newFileId);
                    $fileTag->setTagId($newTagId);
                    
                    $fileTagRepo->addTagToFile($fileTag);
                }
            }
        } catch (Exception $e) {
            error_log("Error importing file tags: " . $e->getMessage());
        }
    }
}