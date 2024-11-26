<?php
require_once "../Repositories/FileTagRepository.php";
require_once "../Models/FileTag.php";

use FileTag\FileTag;

if (isset($_POST['fileId']) && isset($_POST['tagId'])) {
    $fileId = (int) $_POST['fileId'];
    $tagId = (int) $_POST['tagId'];

    $fileTagRepo = new FileTagRepository();

    $fileTag = new FileTag();
    $fileTag->setFileId($fileId);
    $fileTag->setTagId($tagId);

    $result = $fileTagRepo->addTagToFile($fileTag);

    if ($result) {
        header("Location: tags_on_file.php?fileId=$fileId&success=Tag added successfully");
        exit;
    } else {
        header("Location: tags_on_file.php?fileId=$fileId&error=Failed to add tag");
        exit;
    }
} else {
    header("Location: tags_on_file.php?fileId=$fileId&error=Missing fileId or tagId");
    exit;
}