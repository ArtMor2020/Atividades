<?php
require_once '../Database/Database.php';
require_once '../Models/File.php';
require_once '../Models/FileTag.php';
require_once '../Repositories/FileTagRepository.php';

use FileTag\FileTag;

$response = ['success' => false, 'message' => 'Failed to update file.'];

try {
    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        $fileId = isset($_POST['fileId']) ? (int)$_POST['fileId'] : null;
        $fileName = isset($_POST['fileName']) ? $_POST['fileName'] : null;
        $fileType = isset($_POST['fileType']) ? $_POST['fileType'] : null;
        $assignedTags = isset($_POST['assignedTags']) ? $_POST['assignedTags'] : []; 
        $removedTags = isset($_POST['removedTags']) ? $_POST['removedTags'] : [];

        error_log('update_file.php: '.print_r($_POST, true));

        if ($fileId && $fileName && $fileType) {

            $file = new File\File();
            $file->setId($fileId);
            $file->setName($fileName);
            $file->setType($fileType);

            $pdo = getConnection();

            $stmt = $pdo->prepare("UPDATE files SET name = :name, type = :type WHERE id = :id");
            $stmt->bindValue(':name', $file->getName());
            $stmt->bindValue(':type', $file->getType());
            $stmt->bindValue(':id', $file->getId(), PDO::PARAM_INT);

            if ($stmt->execute()) {

                $fileTagRepo = new FileTagRepository();

                foreach ($removedTags as $tagId) {
                    $fileTag = new FileTag($fileId, (int)$tagId);
                    $fileTagRepo->deleteFileTagAssociation($fileTag);
                }

                foreach ($assignedTags as $tagId) {
                    $fileTag = new FileTag($fileId, (int)$tagId);
                    $fileTagRepo->addTagToFile($fileTag);
                }

                $response['success'] = true;
                $response['message'] = 'File and tags updated successfully!';
            } else {
                $response['message'] = 'Error updating file in the database.';
            }
        } else {
            $response['message'] = 'Invalid file data provided.';
        }
    }
} catch (Exception $e) {
    $response['message'] = 'An error occurred: ' . $e->getMessage();
}

echo json_encode($response);

if ($response['success']) {
    header("Location: " . $_SERVER['HTTP_REFERER'] . "?status=success&message=" . urlencode($response['message']));
} else {
    header("Location: " . $_SERVER['HTTP_REFERER'] . "?status=error&message=" . urlencode($response['message']));
}

exit;
?>
