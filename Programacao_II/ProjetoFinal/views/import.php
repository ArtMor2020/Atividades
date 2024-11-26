<?php
require_once("../Repositories/DatabaseRepository.php");
require_once("../Repositories/FileRepository.php");
require_once("../Repositories/TagRepository.php");
require_once("../Repositories/FileTagRepository.php");

if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_FILES['zipFile'])) {
    try {
        $uploadedFile = $_FILES['zipFile'];
        if ($uploadedFile['error'] !== UPLOAD_ERR_OK) {
            throw new Exception("Error uploading the file: " . $uploadedFile['error']);
        }

        $uploadDir = 'C:/laragon/www/ProjetoFinal/temp/';
        if (!is_dir($uploadDir)) {
            mkdir($uploadDir, 0777, true); 
        }

        $tempFilePath = $uploadDir . basename($uploadedFile['name']);
        if (!move_uploaded_file($uploadedFile['tmp_name'], $tempFilePath)) {
            throw new Exception("Failed to move uploaded file.");
        }

        $fileRepo = new FileRepository();
        $tagRepo = new TagRepository();
        $fileTagRepo = new FileTagRepository();
        $databaseRepo = new DatabaseRepository($fileRepo, $tagRepo, $fileTagRepo);

        $result = $databaseRepo->importDatabase($tempFilePath);

        if ($result) {
            echo "Import successful!";
        } else {
            echo "Error during import.";
        }

        unlink($tempFilePath);

    } catch (Exception $e) {
        echo $e->getMessage();
    }
}
?>
