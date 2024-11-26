<?php

require_once '../Repositories/FileRepository.php';

$fileRepo = new FileRepository();

$data = json_decode(file_get_contents('php://input'), true);
$fileId = $data['fileId'] ?? null;

if ($fileId) {
    $result = $fileRepo->deleteFile($fileId);
    echo json_encode($result); 
} else {
    echo json_encode(false); 
}
