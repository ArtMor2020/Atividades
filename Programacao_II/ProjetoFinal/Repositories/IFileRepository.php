<?php

require_once  "../Models/File.php";
use File\File;

interface IFileRepository
{
    public function addFile(File $file);
    public function getAllFiles();
    public function getFile($id);
    public function getFileByName($name);
    public function getFileByType($type);
    public function updateFile(File $file);
    public function deleteFile($id);
}