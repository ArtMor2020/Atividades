<?php

namespace FileTag;

class FileTag
{
    private $fileId = 0;
    private $tagId = 0;


    public function getFileId()
    {
        return $this->fileId;
    }

    public function setFileId($fileId)
    {
        $this->fileId = $fileId;
    }

    public function getTagId()
    {
        return $this->tagId;
    }

    public function setTagId($tagId)
    {
        $this->tagId = $tagId;
    }
}