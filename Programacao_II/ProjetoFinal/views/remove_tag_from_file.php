<?php

require_once '../Repositories/FileTagRepository.php';

use FileTag\FileTag;

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = json_decode(file_get_contents('php://input'), true);
    $fileId = $data['fileId'] ?? null;
    $tagId = $data['tagId'] ?? null;

    if ($fileId && $tagId) {
        $fileTagRepo = new FileTagRepository();
        $fileTag = new FileTag();
        $fileTag->setFileId($fileId);
        $fileTag->setTagId($tagId);

        $result = $fileTagRepo->deleteFileTagAssociation($fileTag);

        if ($result) {
            echo json_encode(['success' => true]);
        } else {
            echo json_encode(['success' => false, 'error' => 'Failed to remove association']);
        }
    } else {
        echo json_encode(['success' => false, 'error' => 'Missing fileId or tagId']);
    }
}

