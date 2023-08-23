#!/bin/bash

# Путь к проекту  
PROJECT_PATH="E:/Users/Forni/Documents/UnityProjects/GridTest"
PROJECT_SETTINGS_PATH="E:/Users/Forni/Documents/UnityProjects/GridTest/ProjectSettings"

# Получаем версию редактора из файла проекта
UNITY_VERSION=$(cat "$PROJECT_SETTINGS_PATH/ProjectVersion.txt" | grep -o 'm_EditorVersion: .*')
UNITY_VERSION="${UNITY_VERSION#*: }"
echo "$UNITY_VERSION"

# Формируем путь к редактору на основе версии
UNITY_PATH="E:/Program Files/Unity/Editor/$UNITY_VERSION/Editor/Unity.exe"

# Остальной код скрипта...

BUILD_PATH="$PROJECT_PATH/Builds"
mkdir -p "$BUILD_PATH" 

"$UNITY_PATH" -batchmode -logFile -debugMode -nographics -quit -projectPath "$PROJECT_PATH" \
-executeMethod Build.CustomBuilder.BuildAndroid
"$UNITY_PATH" -batchmode -logFile -debugMode -nographics -quit -projectPath "$PROJECT_PATH" \
-executeMethod Build.CustomBuilder.BuildWindows

echo "Build completed"

# Читаем ввод пользователя для поддержания работы скрипта
read -p "Press any key to exit"
read -p "Press any key to exit"