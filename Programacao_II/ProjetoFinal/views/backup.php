<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    <style>
        .button-container {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 10px;
            flex-direction: row;
        }

        .btn {
            padding: 10px 16px;
            background-color: #444;
            color: white;
            border: 1px solid #666;
            border-radius: 8px;
            font-size: 18px;
            cursor: pointer;
            text-align: center;
            transition: background-color 0.3s ease, transform 0.3s ease;
            flex: 1;
            margin: 0 10px;
        }

        .btn:hover {
            background-color: #666;
            transform: translateY(-2px);
        }

        .btn:active {
            background-color: #333;
            transform: translateY(1px);
        }

        .btn:focus {
            outline: none;
            box-shadow: 0 0 3px 2px rgba(255, 255, 255, 0.6);
        }

        /* Hide the file input, and style the container for progress */
        #fileInput {
            display: none;
        }

        #progress-container {
            display: none;
            width: 100%;
            margin-top: 10px;
        }

        #progress {
            width: 0;
            height: 10px;
            background-color: green;
        }

        #status {
            margin-top: 10px;
        }
    </style>
</head>
<body>

    <?php include 'topbar.php'; ?>
    <div class="content">
        <div class="button-container">
            <form action="export.php" method="POST">
                <button type="submit" name="export" class="btn">Export</button>
            </form>

            <button id="importButton" class="btn">Import</button>
            <input type="file" id="fileInput" accept=".zip" />
        </div>

        <div id="progress-container">
            <div id="progress"></div>
        </div>
        <div id="status"></div>
    </div>

    <script>
        document.getElementById('importButton').addEventListener('click', function() {
            document.getElementById('fileInput').click();
        });

        document.getElementById('fileInput').addEventListener('change', function(event) {
            let file = event.target.files[0];

            if (!file) {
                alert("Please select a ZIP file.");
                return;
            }

            let formData = new FormData();
            formData.append('zipFile', file);

            document.getElementById('progress-container').style.display = 'block';

            let xhr = new XMLHttpRequest();
            xhr.open('POST', 'import.php', true);

            xhr.upload.onprogress = function(e) {
                if (e.lengthComputable) {
                    let percent = (e.loaded / e.total) * 100;
                    document.getElementById('progress').style.width = percent + '%';
                }
            };

            xhr.onload = function() {
                if (xhr.status === 200) {
                    document.getElementById('status').innerText = 'Import successful!';
                } else {
                    document.getElementById('status').innerText = 'Error during import: ' + xhr.responseText;
                }
                document.getElementById('progress-container').style.display = 'none';
            };

            xhr.onerror = function() {
                document.getElementById('status').innerText = 'An error occurred during the request.';
                document.getElementById('progress-container').style.display = 'none';
            };

            xhr.send(formData);
        });
    </script>
</body>
</html>
