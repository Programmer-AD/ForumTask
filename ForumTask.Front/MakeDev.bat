cd src
cmd /C npm run build-dev
cd ..
mkdir ..\BuildResult\Debug\static
copy index.html ..\BuildResult\Debug\static\index.html
copy result\app.js ..\BuildResult\Debug\static\app.js