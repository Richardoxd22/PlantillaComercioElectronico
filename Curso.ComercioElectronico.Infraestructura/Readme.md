#Las apis tienen relacion lo cual la principal con la cual trabajan en mas relacion es a partir de la 
#Api Cart =>Product(Brand y producttype) => DeliveryMode  y de esta Product => Brand && ProductType


#La busqueda y paginacion por medio de dos partes como entendi la api pueda buscar dos entidades si lo hace
#en Product en la paginacion lo hace por Name y Descripcion || en caso de Cart solo esta con Name 

#Los CRUD principales deben tener un orden para no generar errores Bran y ProductType =>Producto=>DeliveryMode => Cart (General)
#CRUD completas Brand, ProductType, Producto, DeliveryMode y Cart

#En caso de calcular los valores lo realiza en cart mismo con cada uno y devuelve el valor en CartResult

#Ejemplo B; No se completo
#Ejemplo C;No se completo

#En cuanto a la autorizacion van a estar los datos mas comprometidos como es una busqueda directa de Id 
#En cada uno de los controlers para hacer validar
#Se probo con postman estan funcionando con ""userName": "rich", "Admin" "password":"12345"


# Agregar migracion
add-migration Inicial -Context ComercioElectronicoDbContext

# Aplicar migracion
Update-Database -Context ComercioElectronicoDbContext 

# Realizar migracion por script
Script-Migration -Context ComercioElectronicoDbContext -From Inicial

# Genera script desde la primera migracion hasta la ultima
Script-Migration -Context ComercioElectronicoDbContext 0

