<?php

require_once "../Models/FileTag.php";
require_once "../Repositories/FileTagRepository.php";  
require_once "../Database/Database.php";  

header('Content-Type: application/json');

if (!isset($_GET['fileId']) || !is_numeric($_GET['fileId'])) {
    echo json_encode(['error' => 'Invalid or missing fileId parameter']);
    exit;
}

$fileId = $_GET['fileId'];
$fileTagRepo = new FileTagRepository();
$tags = $fileTagRepo->getTagsOnFile($fileId);

error_log('Tags on File: ' . print_r($tags, true));

if ($tags !== false) {
    echo json_encode($tags);
} else {
    echo json_encode([]);
}
?>
