startup-project := ./XUnitBlog.App/
project := ./XUnitBlog.Data/

ef-add:
	dotnet ef migrations add $(name) --project ${project} --startup-project ${startup-project}

ef-remove:
	dotnet ef migrations remove --project ${project} --startup-project ${startup-project}

ef-up: 
	dotnet ef database update --project ${project} --startup-project ${startup-project}

ef-down:
	dotnet ef database drop --project ${project} --startup-project ${startup-project}

run:
	dotnet run --project ${startup-project}