<?php

require_once  "../Models/Tag.php";
require_once  "../Database/database.php";
require_once "../Repositories/ITagRepository.php";

use Tag\Tag;

class TagRepository implements ITagRepository
{
    private $pdo;

    public function __construct() {
        $this->pdo = getConnection();
    }

    function addTag(Tag $tag): Tag|bool   
    {
        
        $this->pdo->beginTransaction();
        
        try{
            $name = $tag->getName();
            $description = $tag->getDescription();

            $stmt1 = $this->pdo->prepare("SELECT * FROM tags WHERE name = :name");
            $stmt1->bindValue(':name', $name);
            $stmt1->execute();

            $tags = $stmt1->fetchAll(PDO::FETCH_ASSOC);

            $this->pdo->commit();

            if(!empty($tags))
            {
                $tag->setId($tags[0]['id']);
                return $tag;
            }

            $this->pdo->beginTransaction();

            $stmt2 = $this->pdo->prepare("INSERT INTO tags (name, description) VALUES (:name, :description)");
            $stmt2->bindValue(':name', $name);
            $stmt2->bindValue(':description', $description);

            if ($stmt2->execute()) {
                $tag->setId($this->pdo->lastInsertId());
                $this->pdo->commit(); 
                return $tag; 
            }

            return false;

        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function getAllTags(): array|bool
    {
        try{
            $stmt = $this->pdo->prepare("SELECT * FROM tags ORDER BY name"); /* add sorting to db query*/
            $stmt->execute();

            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $tags = [];

            foreach ($rows as $row) {
                $tag = new Tag();
                $tag->setId($row['id']);
                $tag->setName($row['name']);
                $tag->setDescription($row['description']);
                $tags[] = $tag; 
            }

            return $tags; 
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function getTag($id): Tag|bool
    {
        try{    
            $stmt = $this->pdo->prepare("SELECT * FROM tags WHERE id = :id");
            $stmt->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt->execute();

            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            if ($row) {
                $tag = new Tag();
                $tag->setId($row['id']);
                $tag->setName($row['name']);
                $tag->setDescription($row['description']);
                return $tag;
            }

            return false; 
        } catch (Exception $e) {
            error_log($e->getMessage()); 
            return false; 
        }
    }

    function getTagByName($name): array|bool
    {
        try {
            $stmt = $this->pdo->prepare("
                SELECT * 
                FROM tags 
                WHERE name LIKE :search_query
            ");
            $search_query = "%" . $name . "%"; 
            $stmt->bindValue(':search_query', $search_query, PDO::PARAM_STR);
            $stmt->execute();

            $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
            $tags = [];

            foreach ($rows as $row) {

                $tag = new Tag();
                $tag->setId($row['id']);
                $tag->setName($row['name']);
                $tag->setDescription($row['description']);

                $levenshteinDistance = levenshtein($name, $row['name']);  // find how well the name matches the search
                $maxLength = max(strlen($name), strlen($row['name']));

                if ($maxLength == 0) {
                    $matchPercentage = 100;
                } else {
                    $matchPercentage = (1 - ($levenshteinDistance / $maxLength)) * 100;
                }

                $tags[] = [
                    'tag' => $tag,
                    'matchPercentage' => round($matchPercentage, 2) 
                ];
            }


            usort($tags, function($a, $b) { // sort tags by how well they match the search
                return $b['matchPercentage'] - $a['matchPercentage']; 
            });

            return array_map(function($tagData) {
                return $tagData['tag'];
            }, $tags);

        } catch (Exception $e) {
            error_log($e->getMessage());
            return false;
        }
    }

    function updateTag(Tag $tag): bool
    {
        $this->pdo->beginTransaction();
        
        try{
            $stmt = $this->pdo->prepare("
                UPDATE tags 
                SET name = :name, description = :description 
                WHERE id = :id
            ");

            $name = $tag->getName();
            $description = $tag->getDescription();
            $id = $tag->getId();
            
            $stmt->bindValue(':name', $name);
            $stmt->bindValue(':description', $description);
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

    function deleteTag($id): bool
    {
        $this->pdo->beginTransaction();
        
        try {
            $stmt1 = $this->pdo->prepare("DELETE FROM fileTags WHERE tagId = :id");
            $stmt1->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt1->execute();

            $stmt2 = $this->pdo->prepare("DELETE FROM tags WHERE id = :id");
            $stmt2->bindValue(':id', $id, PDO::PARAM_INT);
            $stmt2->execute();

            $this->pdo->commit();

            return true; 
            
        } catch (Exception $e) {
            $this->pdo->rollBack();
            error_log($e->getMessage()); 
            return false; 
        }
    }
}   