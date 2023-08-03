<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<br />
<div align="center">

<h3 align="center">Two Folder Synchronizer</h3>
<p align="center">
    A C# console application to synchronize two folders
  </p>
</div>

<!-- ABOUT THE PROJECT -->
## About The Project

A C# console application to do a one-way synchronization between two folders, a source and a replica. The synchronization process will run periodically based on the the delay given. All operations executed during the synchronization process will be written in the log file provided.

<!-- GETTING STARTED -->
## Running the program

1. Create the source folder, the replica folder and the log file if they do not already exist.
2. Prepare the following parameters:
   ```sh
   The path to source folder, the replica folder and the log file.
   The synchronization delay in seconds
   ```
3. Open a terminal window in the project directory
4. In your terminal window, run the executable `TwoFolderSynchronizer.exe` with the data in step 2 as follows:
   ```sh
   .\bin\Debug\net7.0\TwoFolderSynchronizer.exe [PathToSourceFolder] [PathToReplicaFolder] [SynchronizationDelayInSeconds] [PathToLogFile]
   ```
5. Example:
   ```sh
   .\bin\Debug\net7.0\TwoFolderSynchronizer.exe "C:\SourceFolder" "C:\ReplicaFolder" 5 "C:\logfile.txt"
   ```

<!-- CONTACT -->
## Contact

Shady Zahran - zahran.shady@gmail.com

[![LinkedIn][linkedin-shield]][linkedin-url]

Project Link: https://github.com/ShadyZahran/TwoFolderSynchronizer

<!-- MARKDOWN LINKS & IMAGES -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/shadyzahran/

