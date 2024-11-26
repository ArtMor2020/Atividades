<?php
require_once '../Repositories/FileTagRepository.php';
require_once '../Repositories/TagRepository.php'; 

$fileId = isset($_GET['fileId']) ? $_GET['fileId'] : null;

if (!$fileId) {
    echo json_encode(['error' => 'File ID is required']);
    exit;
}

$fileTagRepo = new FileTagRepository();

$availableTags = $fileTagRepo->getAvailableTagsOnFile($fileId);

error_log('Available Tags: ' . print_r($availableTags, true)); 

if ($availableTags === false) {
    echo json_encode(['error' => 'Error fetching available tags']);
} else {
    $tags = array_values($availableTags); 

    echo json_encode($tags);
}
