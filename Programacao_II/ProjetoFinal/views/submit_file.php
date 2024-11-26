<?php

require_once "../Models/File.php";               
require_once "../Repositories/FileRepository.php"; 
require_once "../Database/Database.php";           

use File\File;

$uploadDirectory = "../Files/";

if ($_SERVER['REQUEST_METHOD'] == 'POST' && isset($_FILES['file-upload']) && $_FILES['file-upload']['error'] == 0) {

    $fileTmpPath = $_FILES['file-upload']['tmp_name'];
    $fileName = $_POST['file-name']; 
    $fileType = $_POST['file-type']; 

    $fileExtension = pathinfo($_FILES['file-upload']['name'], PATHINFO_EXTENSION);
    $fileNewName = uniqid('', true) . '.' . $fileExtension;

    $filePath = $uploadDirectory . $fileNewName;

    $file = new File();
    $file->setName($fileName);
    $file->setPath($filePath); 
    $file->setType($fileType);

    $pdo = getConnection();  

    $pdo->beginTransaction();

    try {

        $fileRepo = new FileRepository();
        $result = $fileRepo->addFile($file); 

        if ($result) {
            if (move_uploaded_file($fileTmpPath, $filePath)) {
                $pdo->commit();
                header("Location: Files.php");
                exit; 
            } else {
                $pdo->rollBack();
                header("Location: addFile.php?error=file_move_failed");
                exit;
            }
        } else {
            $pdo->rollBack();
            header("Location: addFile.php?error=db_insert_failed"); 
            exit;
        }
    } catch (Exception $e) {
        $pdo->rollBack();
        error_log($e->getMessage());  
        header("Location: addFile.php?error=upload_failed"); 
        exit;
    }
} else {
    header("Location: addFile.php?error=no_file_uploaded"); 
    exit;
}
