<?php
require_once "../Repositories/ITagRepository.php";
require_once "../Repositories/TagRepository.php";
require_once "../Models/Tag.php";

use Tag\Tag;

$tagName = isset($_POST['tag-name']) ? $_POST['tag-name'] : '';
$tagDescription = isset($_POST['tag-description']) ? $_POST['tag-description'] : '';

if (empty($tagName) || empty($tagDescription)) {
    header('Location: add_tag.php?error=empty_fields&name=' . urlencode($tagName) . '&description=' . urlencode($tagDescription));
    exit;
}

$tag = new Tag();
$tag->setName($tagName);
$tag->setDescription($tagDescription);

$tagRepo = new TagRepository();

$result = $tagRepo->addTag($tag);

if ($result) {
    header('Location: tags.php?success=tag_added');
    exit;
} else {
    header('Location: addtag.php?error=add_failed&name=' . urlencode($tagName) . '&description=' . urlencode($tagDescription));
    exit;
}
?>
