<head>
    <style>
        .rightsidebar {
            position: fixed; 
            top: 50px; 
            right: 0;
            width: 300px;
            height: calc(100vh - 60px);
            background-color: #f4f4f4;
            border-left: 2px solid #ddd;
            padding: 20px;
            box-shadow: -2px 0 10px rgba(0, 0, 0, 0.1);
            display: none;
            z-index: 1000;
            transition: transform 0.3s ease;
        }

        .rightsidebar.open {
            display: block;
            transform: translateX(0);
        }

        .rightsidebar .close-btn {
            background: none;
            border: none;
            font-size: 18px;
            color: #333;
            cursor: pointer;
            position: absolute;
            top: 20px;
            right: 20px;
        }

        .rightsidebar h2 {
            margin-top: 0;
        }

        .rightsidebar input,
        .rightsidebar select {
            width: 100%;
            padding: 8px;
            margin: 10px 0;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        .rightsidebar button {
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 10px;
        }

        .rightsidebar button:hover {
            background-color: #45a049;
        }

        .image-preview {
            margin: 15px 0;
            text-align: center;
            max-width: 100%; 
            max-height: 200px; 
            overflow: hidden; 
            display: flex; 
            justify-content: center; 
            align-items: center; 
        }

        .image-preview img {
            width: 100%; 
            height: 100%; 
            object-fit: contain; 
            max-width: 100%; 
            max-height: 200px; 
            border-radius: 8px;
        }

        .text-preview {
            white-space: pre-wrap;
            word-wrap: break-word;
            margin-top: 15px;
            background: #f4f4f4;
            padding: 10px;
            border-radius: 5px;
            font-family: monospace;
            font-size: 14px;
            max-height: 200px;
            overflow: auto;
        }

        .pdf-preview {
            margin-top: 15px;
            text-align: center;
        }

        .pdf-preview embed {
            width: 100%;
            height: 200px;
        }

        .video-preview {
            margin-top: 15px;
            text-align: center;
            max-width: 100%;
            max-height: 200px; /* Limit the height */
            overflow: hidden;
        }

        .video-preview video {
            width: 100%;
            height: auto;
            object-fit: contain;
            border-radius: 8px;
        }

        .audio-preview {
            width: 100%;
            margin-top: 15px;
            border-radius: 5px;
        }
    </style>
</head>

<body>
    <div class="rightsidebar" id="rightsidebar">
        <button class="close-btn" onclick="closeRightSidebar()">×</button>
        <h2>Edit File</h2>
        
        <div class="file-preview" id="filePreviewContainer">
        </div>

        <form id="fileForm" method="post" action="update_file.php">
            <input type="hidden" name="fileId" id="fileId" value="">
            <input type="hidden" name="fileType" id="fileType" value="">
            
            <label for="fileName">File Name:</label>
            <input type="text" id="fileName" name="fileName" required>

            <button type="submit">Save Changes</button>
            <button type="button" onclick="deleteFile()">Delete File</button>

            <a href="tags_on_file.php?fileId=" id="manageTagsButton">
                <button type="button">Manage Tags</button>
            </a>
        </form>

    </div>
</body>

<script>
    function openRightSidebar(fileId, fileName, filePath, fileType) {
        const sidebar = document.getElementById('rightsidebar');
        if (!sidebar) {
            console.error('Sidebar not found');
            return;
        }

        sidebar.classList.add('open');
        
        document.getElementById('fileId').value = fileId;
        document.getElementById('fileName').value = fileName;
        document.getElementById('fileType').value = fileType;

        const manageTagsButton = document.getElementById('manageTagsButton');
        manageTagsButton.href = `tags_on_file.php?fileId=${fileId}`;

        updateFilePreview(filePath);
    }

    function closeRightSidebar() {
        document.getElementById('rightsidebar').classList.remove('open');
        filePreviewContainer.innerHTML = ''; 
    }

    function updateFilePreview(filePath) {
        console.log('updateFilePreview called');  

        const filePreviewContainer = document.getElementById('filePreviewContainer');
        filePreviewContainer.innerHTML = ''; 

        if (filePath.endsWith('.jpg') || filePath.endsWith('.jpeg') || filePath.endsWith('.png') || filePath.endsWith('.gif')) {
            const imgContainer = document.createElement('div');
            imgContainer.classList.add('image-preview'); 

            const img = document.createElement('img');
            img.src = filePath;
            img.alt = 'File Preview';
            img.classList.add('image'); 

            imgContainer.appendChild(img); 
            filePreviewContainer.appendChild(imgContainer); 

        } else if (filePath.endsWith('.txt')) {
            fetch(filePath)
                .then(response => response.text())  
                .then(text => {
                    const textPreview = document.createElement('div');
                    textPreview.classList.add('text-preview');
                    textPreview.textContent = text;
                    filePreviewContainer.appendChild(textPreview);
                })
                .catch(err => console.error('Error fetching text file:', err));

        } else if (filePath.endsWith('.pdf')) {
            const pdfEmbed = document.createElement('embed');
            pdfEmbed.src = filePath;
            pdfEmbed.type = 'application/pdf';
            pdfEmbed.classList.add('pdf-preview');
            filePreviewContainer.appendChild(pdfEmbed);

        } else if (filePath.endsWith('.mp4') || filePath.endsWith('.webm') || filePath.endsWith('.ogg')) {
            const video = document.createElement('video');
            video.src = filePath;
            video.controls = true;
            video.classList.add('video-preview');
            filePreviewContainer.appendChild(video);

        } else if (filePath.endsWith('.mp3') || filePath.endsWith('.wav') || filePath.endsWith('.ogg')) {
            const audio = document.createElement('audio');
            audio.src = filePath;
            audio.controls = true;
            audio.classList.add('audio-preview');
            filePreviewContainer.appendChild(audio);

        } else {
            filePreviewContainer.innerHTML = 'Preview not available for this file type.';
        }
    }

    function deleteFile() {
        const fileId = document.getElementById('fileId').value;
        if (confirm('Are you sure you want to delete this file?')) {
            fetch('delete_file.php', {
                method: 'POST',
                body: JSON.stringify({ fileId: fileId }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('File deleted successfully!');
                    location.reload(); 
                } else {
                    alert('Failed to delete file.');
                }
            })
            .catch(err => console.log(err));
        }
    }
</script>
