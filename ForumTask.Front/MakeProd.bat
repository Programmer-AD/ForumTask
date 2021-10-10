cd src
cmd /C npm run build-prod
cd ..
mkdir ..\BuildResult\Release\wwwroot
copy index.html ..\BuildResult\Release\wwwroot\index.html
copy result\app.js ..\BuildResult\Release\wwwroot\app.js