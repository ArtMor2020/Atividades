<?php

require_once  "../Models/Tag.php";
use Tag\Tag;

interface ITagRepository
{
    public function addTag(Tag $tag);
    public function getAllTags();
    public function getTag($id);
    public function getTagByName($name);
    public function updateTag(Tag $tag);
    public function deleteTag($id);
}