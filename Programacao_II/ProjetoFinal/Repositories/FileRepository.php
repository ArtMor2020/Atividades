<?php

require_once  "../Models/File.php";
require_once  "../Database/database.php";
require_once "../Repositories/IFileRepository.php";

use File\File;

class FileRepository implements IFileRepository
{
    private $pdo;

    public function __construct() {
        $this->pdo = getConnection();
    }

    function addFile(File $file): File|bool {

        if (empty($file->getName()) || !is_string($file->getName())) return false;
        if (empty($file->getPath()) || !is_string($file->getPath())) return false;
        if (empty($file->getType()) || !is_string($file->getType())) return false;

        $this->pdo->beginTransaction();
        
        try {
            $stmt1 = $this->pdo->prepare("SELECT * FROM files WHERE path = :path");
            $path = $file->getPath();
            $stmt1->bindValue(':path', $path);
            $stmt1->execute();

            $files = $stmt1->fetchAll(PDO::FETCH_ASSOC);

            $this->pdo->commit();

            if (!empty($files)) {
                // Assuming the first result is the one you want to map to the $file object
                $file = new File();

                // Map each column from the database to the corresponding property in the File object
                $fileData = $files[0];
                $file->setId($fileData['id']);
                $file->setName($fileData['name']);
                $file->setPath($fileData['path']);
                $file->setType($fileData['type']);

                return $file;
            }

            $this->pdo->beginTransaction();
            
            $stmt2 = $this->pdo->prepare("INSERT INTO files (name, path, type) VALUES (:name, :path, :type)");
            
            $name = $file->getName();
            $path = $file->getPath();
            $type = $file->getType();

            $stmt2->bindValue(':name', $name);
            $stmt2->bindValue(':path', $path);
            $stmt2->bindValue(':type', $type);

            if ($stmt2->execute()) {
                $file->setId($this->pdo->lastInsertId());
                $this->pdo->commit(); 
                return $file; 
            }

            return false; 

        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function getAllFiles(): array|bool
    {
        try{
            $stmt = $this->pdo->prepare("SELECT * FROM files ORDER BY type, name ASC");
            $stmt->execute();

            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $files = [];

            foreach ($rows as $row) {
                $file = new File();
                $file->setId($row['id']);
                $file->setName($row['name']);
                $file->setPath($row['path']);
                $file->setType($row['type']);
                $files[] = $file; 
            }

            return $files; 
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return false; }
    }

    function getFile($id): bool|File|null
    {
        if (empty($id) || !is_numeric($id)) return false;

        try{
            $stmt = $this->pdo->prepare("SELECT * FROM files WHERE id = :id");
            $stmt->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt->execute();

            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            if ($row) {
                $file = new File();
                $file->setId($row['id']);
                $file->setName($row['name']);
                $file->setPath($row['path']);
                $file->setType($row['type']);
                return $file;
            }

            return null;
        } catch (Exception $e) {
            error_log($e->getMessage());
            return false; }
    }

    function getFileByName($name): array|bool
    {
        if (empty($name) || !is_string($name)) return false;

        try {
            $stmt = $this->pdo->prepare("SELECT * FROM files WHERE name LIKE :search_query");
            $search_query = "%" . $name . "%"; 
            $stmt->bindValue(':search_query', $search_query, PDO::PARAM_STR);
            $stmt->execute();

            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $files = [];

            foreach ($rows as $row) {
                $file = new File();
                $file->setId($row['id']);
                $file->setName($row['name']);
                $file->setPath($row['path']);
                $file->setType($row['type']);

                $levenshteinDistance = levenshtein($name, $row['name']); // find how well the name matches the search
                $maxLength = max(strlen($name), strlen($row['name']));

                if ($maxLength == 0) {
                    $matchPercentage = 100;
                } else {
                    $matchPercentage = (1 - ($levenshteinDistance / $maxLength)) * 100;
                }


                $files[] = [
                    'file' => $file,
                    'matchPercentage' => round($matchPercentage, 2) 
                ];
            }

            usort($files, function ($a, $b) { // sort files by how well they match the search
                return $b['matchPercentage'] - $a['matchPercentage']; 
            });

            return array_map(function ($fileData) {
                return $fileData['file'];
            }, $files);

        } catch (Exception $e) {
            error_log($e->getMessage());
            return false;
        }
    }

    
    function getFileByType($type): array|bool {


        if (empty($type) || !is_string($type)) return false;

        try {
            $stmt = $this->pdo->prepare("SELECT * FROM files WHERE type = :type ORDER BY name");

            $partial_type = '%' . $type . '%';
    
            $stmt->bindValue(':type', $type, PDO::PARAM_STR);
            $stmt->bindValue(':partial_type', $partial_type, PDO::PARAM_STR);
    
            $stmt->execute();
    
            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $files = [];
    

            foreach ($rows as $row) {
                $file = new File();
                $file->setId($row['id']);
                $file->setName($row['name']);
                $file->setPath($row['path']);
                $file->setType($row['type']);
                $files[] = $file;
            }
    
            return $files;
        } catch (Exception $e) {
            error_log($e->getMessage());
            return false;
        }
    }
    

    function updateFile(File $file): bool
    {
        if (empty($file->getId()) || !is_numeric($file->getId())) return false;
        if (empty($file->getName()) || !is_string($file->getName())) return false;
        if (empty($file->getPath()) || !is_string($file->getPath())) return false;
        if (empty($file->getType()) || !is_string($file->getType())) return false;

        $this->pdo->beginTransaction();

        try{
            $stmt = $this->pdo->prepare("
                UPDATE files 
                SET name = :name, type = :type
                WHERE id = :id
            ");

            $name = $file->getName();
            $path = $file->getPath();
            $type = $file->getType();
            $id = $file->getId();

            $stmt->bindValue(':name', $name);
            $stmt->bindValue(':type', $type);
            $stmt->bindValue(':id', $id, PDO::PARAM_INT);

            $stmt->execute();

            $this->pdo->commit();

            return true;
        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function deleteFile($id): bool
    {
        $this->pdo->beginTransaction();
    
        try {
            $stmt = $this->pdo->prepare("SELECT path FROM files WHERE id = :id");
            $stmt->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt->execute();
            $file = $stmt->fetch(PDO::FETCH_ASSOC);
    
            if ($file) {
                $filePath = $file['path']; 

                if (file_exists($filePath)) {
                    unlink($filePath); 
                }
            }

            $stmt2 = $this->pdo->prepare("DELETE FROM fileTags WHERE fileId = :id");
            $stmt2->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt2->execute();

            $stmt1 = $this->pdo->prepare("DELETE FROM files WHERE id = :id");
            $stmt1->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt1->execute();

            $this->pdo->commit();
    
            return true;
        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log('id = '.$id.$e->getMessage());
            return false;
        }
    }
}