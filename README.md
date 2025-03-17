# 🏠 Inmobiliaria2Cuarti

**Inmobiliaria2Cuarti** es una aplicación web desarrollada en **ASP.NET Core MVC** que permite la gestión de **inmuebles, propietarios, inquilinos, contratos y pagos** dentro de una inmobiliaria.

---

## 📌 Características

- 🏠 **Gestión de inmuebles**: Alta, edición y eliminación de propiedades.
- 📑 **Administración de contratos**: Creación, modificación y asignación de contratos a inquilinos.
- 👤 **Gestión de usuarios**: Propietarios, inquilinos y administradores.
- 💰 **Registro de pagos**: Control de pagos asociados a contratos.
- 📊 **Panel de administración**: Visualización de datos y estadísticas inmobiliarias.
- 🔐 **Sistema de autenticación y permisos**: Acceso restringido según roles de usuario.

---

## 🛠️ Tecnologías utilizadas

- **ASP.NET Core MVC**: Desarrollo de la lógica del servidor.
- **Entity Framework Core**: Gestión de base de datos.
- **SQL Server**: Almacenamiento de datos relacionales.
- **Bootstrap**: Diseño responsivo y estilizado.
- **JavaScript (jQuery, Validaciones)**: Interactividad en la interfaz.
- **Firebase**: Configuración y autenticación en la nube.

---

## 📁 Estructura del Proyecto

```bash
Inmobiliaria2Cuarti/
│── Controllers/                  # Controladores de la aplicación
│   ├── AccountController.cs
│   ├── InmuebleController.cs
│   ├── PropietarioController.cs
│   ├── InquilinoController.cs
│   ├── ContratoController.cs
│   ├── PagosController.cs
│── Models/                       # Modelos de datos
│   ├── Inmueble.cs
│   ├── Inquilino.cs
│   ├── Contrato.cs
│   ├── Pagos.cs
│   ├── Usuario.cs
│── Views/                        # Vistas en Razor (.cshtml)
│   ├── Inmueble/
│   ├── Inquilino/
│   ├── Contrato/
│   ├── Pagos/
│   ├── Usuario/
│── Config/                        # Configuración de Firebase
│   ├── FirebaseConfig.cs
│── appsettings.json               # Configuración general
│── Program.cs                      # Punto de entrada de la aplicación
│── wwwroot/                        # Archivos estáticos (CSS, JS, imágenes)
│── inmobiliaria2.sql               # Script de la base de datos
```

---

## 🚀 Instalación y Uso

### 1️⃣ Clonar el repositorio

```sh
git clone https://github.com/Fermin2049/Inmobiliaria2Cuarti.git
cd Inmobiliaria2Cuarti
```

### 2️⃣ Configurar la base de datos

- Importar `inmobiliaria2.sql` en **SQL Server**.
- Ajustar las credenciales en `appsettings.json`.

### 3️⃣ Ejecutar la aplicación

```sh
dotnet run
```

- Acceder en el navegador a **[http://localhost:5000/](http://localhost:5117/)**.

---

![image](https://github.com/user-attachments/assets/f88981d6-f625-4176-9f8b-2066c15f0621)
![image](https://github.com/user-attachments/assets/95ee542f-7138-45b0-a143-f28146778714)
![image](https://github.com/user-attachments/assets/1bd48efe-5d96-4063-b22c-50795d56a71b)
![image](https://github.com/user-attachments/assets/ee68ad12-39fb-4d4e-9839-f3dd2ae2d2d5)





## 📜 Licencia

Este proyecto es de uso libre bajo la licencia **MIT**.

