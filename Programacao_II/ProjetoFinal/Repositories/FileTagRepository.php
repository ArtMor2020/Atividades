<?php

require_once  "../Models/FileTag.php";
require_once "../Repositories/IFileTagRepository.php";
require_once "../Database/Database.php";
require_once "../Repositories/TagRepository.php";
require_once "../Repositories/FileRepository.php";

use FileTag\FileTag;

class FileTagRepository implements IFileTagRepository
{
    private $pdo;

    public function __construct() {
        $this->pdo = getConnection();
    }

    function getAllFileTags(): array|bool
    {
        try {
            $stmt = $this->pdo->prepare("SELECT * FROM fileTags");
            $stmt->execute();
            
            $fileTags = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if (!$fileTags) {
                return false;
            }

            return $fileTags;
        } catch (Exception $e) {
            error_log($e->getMessage());
            return false;
        }
    }

    function addTagToFile(FileTag $fileTag): bool 
    {
        
        $this->pdo->beginTransaction();

        try{
            $fileId = $fileTag->getFileId();
            $tagId = $fileTag->getTagId();

            $stmt = $this->pdo->prepare("SELECT * FROM fileTags WHERE fileId = :fileId AND tagId = :tagId");
            $stmt->bindValue(':fileId', $fileId);
            $stmt->bindValue(':tagId', $tagId);
            $stmt->execute();

            $fileTag = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $this->pdo->commit();

            if(!empty($fileTag)) return true;

            $this->pdo->beginTransaction();

            $stmt = $this->pdo->prepare("INSERT INTO fileTags (fileId, tagId) VALUES (:fileId, :tagId)");
            $stmt->bindValue(':fileId', $fileId);
            $stmt->bindValue(':tagId', $tagId);

            if($stmt->execute()){
                $this->pdo->commit();
                return true;
            }

            return false;
        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function getTagsOnFile($fileId): array|bool
    {
        try {
            $stmt = $this->pdo->prepare("SELECT * FROM fileTags WHERE fileId = :fileId");
            $stmt->bindValue(':fileId', $fileId, PDO::PARAM_INT);
            $stmt->execute();
            
            $tagsOnFile = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if (!$tagsOnFile) {
                return false; 
            }

            $tagRepo = new TagRepository();
            $detailedTags = [];

            foreach ($tagsOnFile as $tagAssoc) {
                $tagDetails = $tagRepo->getTag($tagAssoc['tagId']); 

                if ($tagDetails) {
                    $detailedTags[] = [
                        'tagId' => $tagDetails->getId(),
                        'name' => $tagDetails->getName()
                    ];
                }
            }

            return $detailedTags; 
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return false; 
        }
    } 

    function getAvailableTagsOnFile($fileId): array|bool /* this is a mess, use just a db query */
    {   
        try {
            $tagRepo = new TagRepository();
            $allTags = $tagRepo->getAllTags(); 

            if (!$allTags) return false; 

            $tagsOnFile = $this->getTagsOnFile($fileId);
            
            if (!$tagsOnFile) {
                return array_map(function($tag) {
                    return [
                        'tagId' => $tag->getId(),
                        'name' => $tag->getName(),
                    ];
                }, $allTags);
            }

            $tagIdsOnFile = array_map(function($tag) {
                return $tag['tagId']; 
            }, $tagsOnFile);

            $availableTags = array_filter($allTags, function($tag) use ($tagIdsOnFile) {
                return !in_array($tag->getId(), $tagIdsOnFile); 
            });

            $availableTagsArray = array_map(function($tag) {
                return [
                    'tagId' => $tag->getId(),
                    'name' => $tag->getName(),
                ];
            }, $availableTags);

            return $availableTagsArray; 
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return false; 
        }
    }

    public function getFilesOnTag($tagId): array
    {
        try {
            $stmt = $this->pdo->prepare("SELECT * FROM fileTags WHERE tagId = :tagId");
            $stmt->bindValue(':tagId', $tagId, PDO::PARAM_INT);
            $stmt->execute();

            $fileTags = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if (!$fileTags) return [];

            $files = [];

            $fileRepository = new FileRepository(); 

            foreach ($fileTags as $fileTag) {

                $file = $fileRepository->getFile($fileTag['fileId']);
                if ($file) {
                    $files[] = $file; 
                }
            }

            return $files;
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return []; 
        }
    }

    function deleteFileTagAssociation(FileTag $fileTag): bool
    {
        $this->pdo->beginTransaction();
        
        
        try {
            $stmt = $this->pdo->prepare("DELETE FROM fileTags WHERE fileId = :fileId AND tagId = :tagId");

            $fileId = $fileTag->getFileId();
            $tagId = $fileTag->getTagId();

            $stmt->bindParam(':fileId', $fileId, PDO::PARAM_INT);
            $stmt->bindParam(':tagId', $tagId, PDO::PARAM_INT);
    
            $result = $stmt->execute(); 
    

            $this->pdo->commit();
    
            return $result; 
        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }
}