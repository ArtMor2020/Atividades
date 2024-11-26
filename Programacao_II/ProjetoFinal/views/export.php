<?php
require_once '../Repositories/FileRepository.php';
require_once '../Repositories/TagRepository.php';
require_once '../Repositories/FileTagRepository.php';
require_once '../Repositories/DatabaseRepository.php';

$fileRepo = new FileRepository();
$tagRepo = new TagRepository();
$fileTagRepo = new FileTagRepository();

$databaseRepo = new DatabaseRepository($fileRepo, $tagRepo, $fileTagRepo);

try {
    $databaseRepo->exportDatabase();
    
    echo '<script type="text/javascript">
            setTimeout(function() {
                window.close();
            }, 2000);  // Close the window after 2 seconds to give time for the download to start
          </script>';
} catch (Exception $e) {
    echo 'Error: ' . $e->getMessage();
    echo '<script type="text/javascript"> setTimeout(function() {window.close(); }, 2000);   </script>'; 
}
