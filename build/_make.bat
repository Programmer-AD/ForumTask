dotnet publish ../backend/ForumTask.PL -o ./publish -c %1

cmd /C npm --prefix ../frontend/src/ run build-%2
xcopy /E /I /y ..\frontend\publish .\publish\wwwroot
