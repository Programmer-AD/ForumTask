cd src
cmd /C npm run build-dev
cd ..
mkdir ..\BuildResult\Debug\wwwroot
copy index.html ..\BuildResult\Debug\wwwroot\index.html
copy result\app.js ..\BuildResult\Debug\wwwroot\app.js
xcopy /E /I /y images ..\BuildResult\Debug\wwwroot\images
