<?php

require_once "../Models/Tag.php";
require_once "../Repositories/TagRepository.php"; 
require_once "../Database/database.php"; 

use Tag\Tag;

$tagRepository = new TagRepository();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {

    $tagId = isset($_POST['tagId']) ? (int)$_POST['tagId'] : 0;
    $tagName = isset($_POST['tagName']) ? $_POST['tagName'] : '';
    $tagDescription = isset($_POST['tagDescription']) ? $_POST['tagDescription'] : '';

    if ($tagId > 0 && !empty($tagName) && !empty($tagDescription)) {

        $tag = new Tag();
        $tag->setId($tagId);
        $tag->setName($tagName);
        $tag->setDescription($tagDescription);


        $updated = $tagRepository->updateTag($tag);

        if ($updated) {
            header('Location: ' . $_SERVER['HTTP_REFERER']);
            exit; 
        } else {
            $errorMessage = 'Failed to update the tag. Please try again.';
        }
    } else {
        $errorMessage = 'Invalid input data. Please check the form fields.';
    }
}

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Update Tag</title>
</head>
<body>

    <?php if (isset($errorMessage)) : ?>
        <div style="color: red;"><?= htmlspecialchars($errorMessage) ?></div>
    <?php endif; ?>

</body>
</html>
