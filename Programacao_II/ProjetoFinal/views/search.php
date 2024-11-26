<?php
require "../Models/File.php";
require "../Models/Tag.php";
require "../Models/FileTag.php";
require "../Database/Database.php";
require "../Repositories/FileRepository.php";
require "../Repositories/TagRepository.php";
require "../Repositories/FileTagRepository.php";

$tagRepo = new TagRepository();
$fileRepo = new FileRepository();
$fileTagRepo = new FileTagRepository();

use Tag\Tag;
use File\File;

$query = isset($_GET['query']) ? $_GET['query'] : '';

$tags = $tagRepo->getTagByName($query);

$files = $fileRepo->getFileByName($query);

$filesByType = [];
foreach ($files as $file) {
    $type = $file->getType(); 
    if (!isset($filesByType[$type])) {
        $filesByType[$type] = [];
    }
    $filesByType[$type][] = $file;
}

function getFileCountForTag($tagId) {
    global $fileTagRepo;
    $fileAssociations = $fileTagRepo->getFilesOnTag($tagId);
    if ($fileAssociations == false) return 0;
    return count($fileAssociations);
}

foreach ($tags as $tag) {
    $tag->fileCount = getFileCountForTag($tag->getId());
}

usort($tags, function($a, $b) {
    if ($a->fileCount == $b->fileCount) {
        return strcmp($a->getName(), $b->getName()); 
    }
    return $b->fileCount - $a->fileCount;
});

function highlightMatch($tagName, $query) {
    if (empty($query)) {
        return htmlspecialchars($tagName);
    }
    $escapedTagName = htmlspecialchars($tagName);
    $escapedQuery = htmlspecialchars($query);
    return preg_replace('/(' . preg_quote($escapedQuery, '/') . ')/i', '<strong>$1</strong>', $escapedTagName);
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Search Results - TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    
    <style>
        .search-content {
            margin-left: 220px; 
            margin-right: 320px;
            padding: 20px;
            flex-grow: 1; 
        }

        .search-tags-list {
            margin-bottom: 20px;
        }

        .search-tag-item {
            background-color: #f4f4f4;
            padding: 10px;
            margin: 5px 0;
            border-radius: 5px;
        }

        .search-tag-name {
            font-weight: bold;
        }

        .search-tag-count {
            font-size: 0.9em;
            color: #888;
        }

        .search-files-list {
            margin-top: 20px;
        }

        .search-files-list ul {
            list-style-type: none;
            padding: 0;
        }

        .search-files-list li {
            background-color: #f4f4f4;
            padding: 8px;
            margin: 5px 0;
            border-radius: 5px;
        }

        .file-type-section {
            margin-bottom: 30px;
        }

        .file-gallery {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
        }

        .file-item {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 10px;
            width: 150px;
            text-align: center;
            cursor: pointer;
        }

        .file-thumbnail img {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 4px;
        }

        .file-details {
            margin-top: 10px;
        }

        .tags {
            margin-top: 10px;
        }

        .tag {
            display: inline-block;
            background-color: #ddd;
            padding: 5px;
            margin: 2px;
            border-radius: 4px;
            font-size: 12px;
        }
        .tags-list {
            margin-top: 20px;
        }

        .tag-item {
            background-color: #f4f4f4;
            padding: 10px;
            margin: 5px 0;
            border-radius: 5px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            cursor: pointer;
        }

        .tag-item:hover {
            background-color: #e0e0e0;
        }

        .tag-name {
            font-weight: bold;
            font-size: 16px;
        }

        .file-count {
            font-size: 14px;
            color: #888;
        }
        
        .edit-buttons {
            display: flex;
            justify-content: flex-end;
            gap: 5px;
        }

        .view-files-button, .edit-button {
            background-color: #4CAF50;
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .view-files-button:hover, .edit-button:hover {
            background-color: #45a049;
        }

        .edit-button {
            background-color: #2196F3;
        }

        .edit-button:hover {
            background-color: #0b7dda;
        }

    </style>
</head>
<body>

    <?php include 'topbar.php'; ?>
    

    <div class="search-content">
        <h1>Search Results for: "<?php echo htmlspecialchars($query); ?>"</h1>
        <div class="tags-list">
            <?php if ($tags): ?>
                <?php foreach ($tags as $tag): ?>
                    <?php
                    $highlightedTagName = highlightMatch($tag->getName(), $query);
                    $fileCount = getFileCountForTag($tag->getId());
                    ?>
                    <div class="tag-item">
                        <div class="tag-info" onclick="openTagRightSidebar(<?php echo $tag->getId(); ?>, '<?php echo addslashes($tag->getName()); ?>', '<?php echo addslashes($tag->getDescription()); ?>')">
                            <span class="tag-name"><?php echo $highlightedTagName; ?></span>
                            <span class="file-count">(<?php echo $tag->fileCount; ?> file<?php echo $tag->fileCount > 1 ? 's' : ''; ?>)</span>
                        </div>
                        
                        <div class="edit-buttons">
                            <a href="view_files.php?tagId=<?php echo $tag->getId(); ?>" class="view-files-button">View Files</a>

                            <button onclick="openTagRightSidebar(<?php echo $tag->getId(); ?>, '<?php echo addslashes($tag->getName()); ?>', '<?php echo addslashes($tag->getDescription()); ?>')" class="edit-button">Edit</button>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No tags found.</p>
            <?php endif; ?>
        </div>
        
        <?php foreach ($filesByType as $type => $files): ?>
            <div class="file-type-section">
                <h2><?php echo ucfirst($type); ?> Files</h2>
                <div class="file-gallery">
                    <?php foreach ($files as $file): ?>
                        <?php
                        $tags = $fileTagRepo->getTagsOnFile($file->getId());
                        ?>
                        <div class="file-item" onclick="openRightSidebar(<?php echo $file->getId(); ?>, '<?php echo htmlspecialchars($file->getName()); ?>', '<?php echo htmlspecialchars($file->getPath()); ?>', '<?php echo htmlspecialchars($file->getType()); ?>')">
                            <div class="file-thumbnail">
                                <?php
                                if ($type === 'image' && file_exists($file->getPath())):
                                    echo "<img src='" . htmlspecialchars($file->getPath()) . "' alt='" . htmlspecialchars($file->getName()) . "' class='file-image'>";
                                else:
                                    echo "<img src='default-icon.png' alt='File' class='file-icon'>"; 
                                endif;
                                ?>
                            </div>
                            <div class="file-details">
                                <h3><?php echo htmlspecialchars($file->getName()); ?></h3>
                                <?php if ($tags): ?>
                                    <div class="tags">
                                        <?php foreach ($tags as $tag): ?>
                                            <span class="tag"><?php echo htmlspecialchars($tag['name']); ?></span>
                                        <?php endforeach; ?>
                                    </div>
                                <?php endif; ?>
                            </div>
                        </div>
                    <?php endforeach; ?>
                </div>
            </div>
        <?php endforeach; ?>
    </div>

    <?php include 'rightsidebar.php'; ?>
    <?php include 'tagrightsidebar.php'; ?>


<script>

    function fetchTags(fileId) {

        fetch('get_tags.php?fileId=' + fileId)
            .then(response => response.json())
            .then(data => {
                const tagList = document.getElementById('tagList');
                tagList.innerHTML = ''; 
                
                if (data && data.length > 0) {
                    data.forEach(tag => {
                        const tagElement = document.createElement('span');
                        tagElement.classList.add('tag');
                        tagElement.textContent = tag.name;
                        tagList.appendChild(tagElement);
                    });
                } else {
                    tagList.innerHTML = 'No tags assigned.';
                }
            })
            .catch(err => console.log(err));
    }

    function deleteFile() {
        const fileId = document.getElementById('fileId').value;
        fetch('delete_file.php', {
            method: 'POST',
            body: JSON.stringify({ fileId: fileId }),
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data === true) { 
                closeRightSidebar(); 
                location.reload(); 
            } else { 
                alert('Failed to delete file.');
            }
        })
        .catch(err => {
            console.log('Error:', err);
            alert('An error occurred while deleting the file.');
        });
    }

</script>

</body>
</html>
