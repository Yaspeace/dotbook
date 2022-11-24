Для сохранения (и хранения) файлов изображений и pdf используется отдельный сервер (в данном случае openserver на localhost), на котором развернут микро-сервер на php с тпеим кодом:
```
<?php
$id = $_POST['id'];
$postfix = $_POST['postfix'];
$isImage = $_POST['isImage'];
$dir = $isImage ? '/dotimages' : '/dotfiles';
$filename = '/file' . $id . '.' . $postfix;
$path = $dir . $filename;
$root = $_SERVER['DOCUMENT_ROOT'] . '/files';
if (move_uploaded_file($_FILES['file']['tmp_name'], $root . $path)) {
    echo $path;
} else {
    http_response_code(400);
    echo "Ошибка сохранения файла";
}
```
Также пока пришлось добавить алиас host.docker.internal => localhost иначе не робобтоет(((