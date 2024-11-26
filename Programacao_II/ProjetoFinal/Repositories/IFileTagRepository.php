<?php

require_once  "../Models/FileTag.php";
use FileTag\FileTag;

interface IFileTagRepository
{
    public function addTagToFile(FileTag $fileTag);
    public function getTagsOnFile($id);
    public function getAvailableTagsOnFile($fileId);
    public function getFilesOnTag($id);
    public function deleteFileTagAssociation(FileTag $fileTag);

}