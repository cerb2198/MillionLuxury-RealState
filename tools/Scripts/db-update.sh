PROJECT = ${1:-"MillionLuxury.RealEstate.Infrastructure"}
STARTUP_PROJECT = ${2:-"MillionLuxury.RealEstate.API"}
DB_CONTEXT = ${3:-"RealStateDbContext"}

dotnet ef database update --project $PROJECT --startup-project $STARTUP_PROJECT
