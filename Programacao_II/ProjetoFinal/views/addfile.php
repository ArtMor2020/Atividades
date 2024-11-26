<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Add File - TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    
    <style>
        .add-file-form {
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            padding: 20px; 
        }

        .add-file-form h1 {
            color: #333;
        }

        .add-file-form form {
            display: flex;
            flex-direction: column;
        }

        .add-file-form label {
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .add-file-form input[type="text"],
        .add-file-form input[type="file"],
        .add-file-form textarea,
        .add-file-form select {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin-bottom: 15px;
            font-size: 16px;
        }

        .add-file-form textarea {
            resize: vertical; 
        }

        .add-file-form button {
            padding: 10px 15px;
            background-color: #333;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

        .add-file-form button:hover {
            background-color: #555;
        }
    </style>
</head>

<body>
    <?php include 'topbar.php'; ?>

    <div class="content add-file-form">
        <h1>Upload Your File</h1>

        <?php if (isset($_GET['error'])): ?>
            <p class="error">
                <?php
                if ($_GET['error'] == 'file_move_failed') {
                    echo "There was an error moving the file to the destination folder. Please try again.";
                } elseif ($_GET['error'] == 'db_insert_failed') {
                    echo "There was an error adding the file to the database. Please try again.";
                } elseif ($_GET['error'] == 'upload_failed') {
                    echo "An unexpected error occurred during the file upload. Please try again.";
                } elseif ($_GET['error'] == 'no_file_uploaded') {
                    echo "No file was uploaded. Please choose a file to upload.";
                }
                ?>
            </p>
        <?php endif; ?>
        
        <form action="submit_file.php" method="post" enctype="multipart/form-data"> 
            <label for="file-upload">Select File:</label>
            <input type="file" id="file-upload" name="file-upload" accept="*/*" required>
            <br>
            
            <div id="file-details" style="display: none;">
                <label for="file-name">Name:</label>
                <input type="text" id="file-name" name="file-name" placeholder="Enter file name" required>
                <br>
                
                <label for="file-type">Type:</label>
                <select id="file-type" name="file-type" required>
                    <option value="">Select a type</option>
                    <option value="audio">Songs</option>
                    <option value="video">Movies</option>
                    <option value="series">Series</option>
                    <option value="book">Books</option>
                    <option value="comic">Comics</option>
                    <option value="image">Images</option>
                    <option value="document">Documents</option>
                    <option value="other">Others</option>
                </select>
                <br>
            </div>
            
            <div id="file-preview" style="display: none;">
                <h3>File Preview:</h3>
                <img id="image-preview" src="" alt="Image Preview" style="display: none; max-width: 300px;">
                <pre id="text-preview" style="display: none;"></pre>
                <p id="preview-message"></p>
            </div>

            <button type="submit">Upload File</button>
        </form>
    </div>

    <script>
    document.getElementById('file-upload').addEventListener('change', function(event) {
        const file = event.target.files[0];
        const fileDetails = document.getElementById('file-details');
        const filePreview = document.getElementById('file-preview');
        const imagePreview = document.getElementById('image-preview');
        const textPreview = document.getElementById('text-preview');
        const previewMessage = document.getElementById('preview-message');

        if (file) {
            fileDetails.style.display = 'block';
            document.getElementById('file-name').value = file.name;

            const fileExtension = file.name.split('.').pop().toLowerCase();
            const fileTypeSelect = document.getElementById('file-type');

            fileTypeSelect.value = '';
            imagePreview.style.display = 'none';
            textPreview.style.display = 'none';
            previewMessage.textContent = '';

            if (['mp3', 'wav', 'aac'].includes(fileExtension)) {
                fileTypeSelect.value = 'audio';
                previewMessage.textContent = "Audio files cannot be previewed.";
            } else if (['mp4', 'mkv', 'avi'].includes(fileExtension)) {
                fileTypeSelect.value = 'video';
                previewMessage.textContent = "Video files cannot be previewed.";
            } else if (['jpg', 'jpeg', 'png', 'gif'].includes(fileExtension)) {
                fileTypeSelect.value = 'image';
                const reader = new FileReader();
                reader.onload = function(e) {
                    imagePreview.src = e.target.result;
                    imagePreview.style.display = 'block';
                }
                reader.readAsDataURL(file);
            } else if (['pdf', 'doc', 'docx', 'txt'].includes(fileExtension)) {
                fileTypeSelect.value = 'document';
                if(!(['pdf'].includes(fileExtension))){
                    const reader = new FileReader();
                    reader.onload = function(e) {
                        textPreview.textContent = e.target.result;
                        textPreview.style.display = 'block';
                    }
                }
                reader.readAsText(file);
            } else {
                fileTypeSelect.value = 'other';
                previewMessage.textContent = "Other file types cannot be previewed.";
            }

            filePreview.style.display = 'block';
        } else {
            fileDetails.style.display = 'none';
            filePreview.style.display = 'none';
            document.getElementById('file-name').value = '';
            fileTypeSelect.value = '';
            imagePreview.src = '';
            textPreview.style.display = 'none';
            previewMessage.textContent = '';
        }
    });
    </script>
</body>
</html>