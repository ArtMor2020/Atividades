<?php

require_once '../Repositories/TagRepository.php';

$tagRepo = new TagRepository(); 

$data = json_decode(file_get_contents('php://input'), true);
$tagId = $data['tagId'] ?? null;

if ($tagId) {
    $result = $tagRepo->deleteTag($tagId);
    echo json_encode($result); 
} else {
    echo json_encode(false);
}
