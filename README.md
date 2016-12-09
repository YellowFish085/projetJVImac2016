# projetJVImac2016

## Créer son projet Unity
:warning: Je tiens à préciser qu'ayant créé le projet **je n'ai pas eu a essayer de l'importer**. Les étapes décrites ci-dessous me semblent les plus logiques pour commencer le travail avec l'ensemble des fichiers que j'ai créé dans le repo mais **je n'ai pas vérifié**. 

**Dans le pire des cas un zip sera uploadé sur notre Drive**.

1. Cloner le repo dans un répertoire.
2. Ouvrir Unity et créer un nouveau projet nommé "Possession" dans le répertoire nouvellement créé suite au git clone.
3. Lancer un git pull sur la branche scene_test pour récupèrer scenes, assets, metadatas, ...
4. Normalement ça devrait être OK :+1:

## Adapter son projet au versionning GIT
1. (Skip this step in v4.5 and up) Enable External option in Unity → Preferences → Packages → Repository.
2. Switch to Visible Meta Files in Edit → Project Settings → Editor → Version Control Mode.
3. Switch to Force Text in Edit → Project Settings → Editor → Asset Serialization Mode.
4. Save the scene and project from File menu.

+ d'infos sur :
- https://docs.unity3d.com/Manual/Versioncontrolintegration.html
- https://stackoverflow.com/questions/18225126/how-to-use-git-for-unity-source-control
