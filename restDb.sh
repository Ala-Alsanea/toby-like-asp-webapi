echo "truncate-db"
echo
dotnet run truncate-db

echo "rm migration"
echo
rm -rf Migrations/

echo "create migration"
echo
dotnet ef migrations add testRestDB

echo "update db"
echo
dotnet-ef database update

#echo "seed-db"
#echo
#dotnet run seed-db