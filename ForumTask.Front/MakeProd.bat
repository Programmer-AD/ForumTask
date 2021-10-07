cd src
cmd /C npm run build-prod
cd ..
mkdir ..\BuildResult\Release\static
copy index.html ..\BuildResult\Release\static\index.html
copy result\app.js ..\BuildResult\Release\static\app.js