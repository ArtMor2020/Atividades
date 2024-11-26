<?php

namespace File;

class File
{
    private $Id = 0;
    private $Name = ""; // file name
    private $Path = ""; // file path
    private $Type = ""; // image, song, movie, series, book, comic, etc


    public function getId()
    {
        return $this->Id;
    }
    
    public function setId($Id)
    {
        $this->Id = $Id;
    }

    public function getName()
    {
        return $this->Name;
    }

    public function setName($Name)
    {
        $this->Name = $Name;
    }

    public function getPath()
    {
        return $this->Path;
    }

    public function setPath($Path)
    {
        $this->Path = $Path;
    }

    public function getType()
    {
        return $this->Type;
    }

    public function setType($Type)
    {
        $this->Type = $Type;
    }
}