-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-09-2024 a las 13:47:37
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria2`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `IdContrato` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL,
  `IdInquilino` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `MontoRenta` int(60) NOT NULL,
  `Deposito` int(50) NOT NULL,
  `Comision` int(50) NOT NULL,
  `Condiciones` varchar(100) NOT NULL,
  `MultaTerminacionTemprana` decimal(10,2) DEFAULT NULL,
  `FechaTerminacionTemprana` date DEFAULT NULL,
  `UsuarioCreacion` varchar(255) DEFAULT NULL,
  `UsuarioTerminacion` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`IdContrato`, `IdInmueble`, `IdInquilino`, `FechaInicio`, `FechaFin`, `MontoRenta`, `Deposito`, `Comision`, `Condiciones`, `MultaTerminacionTemprana`, `FechaTerminacionTemprana`, `UsuarioCreacion`, `UsuarioTerminacion`) VALUES
(1, 2, 1, '2024-09-01', '2024-09-30', 12132, 2000, 25000, 'Renovado', NULL, NULL, NULL, NULL),
(2, 1, 1, '2025-02-01', '2025-01-31', 300000, 600000, 50000, 'Renovado', NULL, NULL, NULL, NULL),
(7, 3, 5, '2024-04-01', '2024-11-30', 75000, 15000, 6000, 'Renovado', NULL, NULL, NULL, NULL),
(8, 1, 1, '2025-01-01', '2025-01-30', 100000, 50000, 5000, 'Renovado', NULL, NULL, NULL, NULL),
(10, 7, 7, '2024-09-26', '2024-10-12', 300000, 600000, 50000, 'Cancelado', 1800000.00, '2024-09-25', NULL, '16'),
(11, 2, 5, '2024-12-01', '2024-12-31', 300000, 600000, 50000, 'copada', NULL, NULL, NULL, NULL),
(12, 5, 1, '2025-03-01', '2025-03-31', 666666, 666666, 777, 'expectacular', NULL, NULL, NULL, NULL),
(13, 8, 1, '2024-10-01', '2024-10-30', 150000, 75000, 30000, 'a estrenar', NULL, NULL, NULL, NULL),
(14, 8, 5, '2024-11-01', '2024-11-30', 777777, 3333, 2222, 'Cancelado', 777777.00, '2024-09-25', '16', '16'),
(15, 2, 1, '2025-03-24', '2025-04-01', 10000, 600000, 50000, 'Cancelado', 110000.00, '2024-09-25', '16', '16'),
(16, 8, 1, '2024-09-01', '2024-09-30', 123456, 123456, 123456, 'fgdsfdsfds', NULL, NULL, '16', NULL),
(17, 4, 1, '2024-09-01', '2026-09-01', 100000, 50000, 25000, 'Cancelado', 600000.00, '2024-09-25', NULL, '16'),
(18, 1, 1, '2024-09-01', '2024-09-30', 300000, 666666, 5000, 'Nuevo', NULL, NULL, NULL, NULL),
(20, 5, 1, '2025-04-01', '2027-04-01', 999999, 88888, 7777, 'Nuevo', NULL, NULL, '16', NULL),
(21, 2, 7, '2025-03-24', '2025-04-01', 4444444, 22222, 1111, 'Nuevo', NULL, NULL, '16', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `IdInmueble` int(10) NOT NULL,
  `IdPropietario` int(10) NOT NULL,
  `Direccion` varchar(50) NOT NULL,
  `Uso` varchar(50) NOT NULL,
  `Tipo` int(50) NOT NULL,
  `CantAmbiente` int(10) NOT NULL,
  `Valor` int(50) NOT NULL,
  `Disponible` tinyint(1) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`IdInmueble`, `IdPropietario`, `Direccion`, `Uso`, `Tipo`, `CantAmbiente`, `Valor`, `Disponible`, `Estado`) VALUES
(1, 1, '123 Calle Ficticia', 'otros', 1, 3, 150000, 1, 1),
(2, 2, '456 Calle Imaginaria', 'Personal', 1, 5, 300000, 1, 1),
(3, 1, 'Calle Falsa 123', 'Residencial', 2, 3, 150000, 0, 1),
(4, 2, 'Avenida Siempre Viva 742', 'Comercial', 2, 1, 100000, 0, 1),
(5, 15, 'Ilusión 1234', 'Trabajo', 3, 3, 1000000, 1, 1),
(6, 17, 'por alla lejos 02', 'coemrcial', 4, 3, 500000, 0, 0),
(7, 11, 'por alla lejos 03', 'coemrcial', 2, 5, 800000, 0, 1),
(8, 13, 'Rivaravia 999', 'vivienda', 1, 5, 350000, 0, 1),
(9, 19, 'por alla lejos 02', 'coemrcial', 1, 456, 345, 0, 1),
(10, 24, 'Rivaravia 999', 'otros', 3, 5, 5000, 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `IdInquilino` int(11) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Dni` int(50) NOT NULL,
  `Telefono` varchar(100) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`IdInquilino`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Estado`) VALUES
(1, 'Fermin', 'Fernandez', 33539061, '2147483647', 'fermin2049@gmail.com', 1),
(4, 'Juan', 'Pérez', 12345678, '1234567890', 'juan.perez@example.com', 1),
(5, 'María', 'González', 87654321, '0987654321', 'maria.gonzalez@example.com', 1),
(7, 'prueba 01', '01', 33001002, '2664055215', 'prueba0101@gmail.com', 1),
(8, 'prueba 02', '02', 2020202, '2664010203', 'prueba02@gmail.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `IdPago` int(11) NOT NULL,
  `IdContrato` int(11) NOT NULL,
  `NroPago` int(10) NOT NULL,
  `FechaPago` date NOT NULL,
  `Detalle` varchar(50) NOT NULL,
  `Importe` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `UsuarioCreacion` varchar(50) NOT NULL,
  `UsuarioAnulacion` varchar(50) NOT NULL,
  `UsuarioEliminacion` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`IdPago`, `IdContrato`, `NroPago`, `FechaPago`, `Detalle`, `Importe`, `Estado`, `UsuarioCreacion`, `UsuarioAnulacion`, `UsuarioEliminacion`) VALUES
(9, 1, 0, '2024-09-23', 'efectivo', 60000.00, 0, '16', '16', NULL),
(10, 10, 10, '2024-09-24', 'devito', 300000.00, 1, 'UsuarioCreacion', '', NULL),
(11, 10, 11, '2024-09-24', 'devito', 55550.00, 1, '16', '', NULL),
(12, 7, 0, '2024-09-25', 'Credito', 75000.00, 0, '', '', NULL),
(13, 14, 0, '2024-09-25', 'Transferencia', 777777.00, 0, '', '', NULL),
(14, 17, 0, '2024-09-25', 'Transferencia', 100000.00, 0, '', '', NULL),
(15, 17, 15, '2024-09-25', 'Efectivo', 100000.00, 1, '16', '', NULL),
(16, 17, 16, '2024-09-25', 'Debito', 600000.00, 1, '16', '', NULL),
(17, 10, 17, '2024-09-25', 'Credito', 1800000.00, 1, '16', '', NULL),
(18, 15, 18, '2024-09-25', 'Transferencia', 110000.00, 1, '16', '', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `IdPropietario` int(10) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Dni` int(20) NOT NULL,
  `Telefono` varchar(50) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`IdPropietario`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Estado`) VALUES
(1, 'Juan 02', 'Pérez', 12345678, '555-1234', 'juan.perez@example.com', 1),
(2, 'María', 'González', 23456789, '555-5678', 'maria.gonzalez@example.com', 1),
(3, 'Carlos', 'Rodríguez', 34567890, '555-2345', 'carlos.rodriguez@example.com', 1),
(6, 'Sofía', 'Gómez', 67890123, '555-5679', 'sofia.gomez@example.com', 1),
(8, 'Laura', 'Hernández', 89012345, '555-7890', 'laura.hernandez@example.com', 1),
(9, 'Pedro', 'Jiménez', 90123456, '555-8901', 'pedro.jimenez@example.com', 1),
(10, 'Lucía', 'Díaz', 12309876, '555-9012', 'lucia.diaz@example.com', 1),
(11, 'Diego', 'Castro', 23409876, '555-0123', 'diego.castro@example.com', 1),
(12, 'Elena', 'Morales', 34509876, '555-3456', 'elena.morales@example.com', 1),
(13, 'Javier', 'Romero', 45609876, '555-4567', 'javier.romero@example.com', 1),
(15, 'Raúl', 'Ramos', 67809876, '555-6789', 'raul.ramos@example.com', 1),
(17, 'José', 'Molina', 89009876, '555-1238901', 'jose.molina@example.com', 1),
(19, 'Andrés', 'Cruz', 23456780, '555-1230', 'andres.cruz@example.com', 1),
(21, 'ENRIQUE ROLANDO', 'GODOY', 78901234, '2664010204', 'GODOY4695@GMAIL.COM', 0),
(22, 'dsdsd', 'bgbgbg', 25987321, '789632514', 'v@j.com', 0),
(23, 'Pichi', 'Papuchi 02', 33001002, '2664000001', 'pichi@gmail.com', 0),
(24, 'Fermin', 'Fernandez', 33539061, '2664297704', 'fermin2049@gmail.com', 1),
(27, 'pepe 03', 'GODOY', 335260564, '02050604', 'fermin2049@gmail.com.ar', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `IdUsuario` int(10) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Contrasenia` varchar(60) DEFAULT NULL,
  `Avatar` varchar(255) DEFAULT NULL,
  `Rol` int(11) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`IdUsuario`, `Nombre`, `Apellido`, `Email`, `Contrasenia`, `Avatar`, `Rol`, `Estado`) VALUES
(1, 'Fermin', 'Fernandez', 'fermin2049@gmail.com', '$2a$11$DyG88DXOt/7x2DaQHrwiY.ZbNwnqzQYRnx3myFqt7TPWxM0/SjmFS', '', 1, 0),
(2, 'Tomas', 'Fernandez', 'Tomas2049@gmail.com', 'asdsads', 'asdsadsa', 1, 1),
(3, 'ENRIQUE ROLANDO', 'GODOY', 'GODOY4695@GMAIL.COM', '$2a$11$wJYoIApbgt/JX/tsIXj2Q.3h3wUr.FDj3J17KyOZOQa', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fcat.jpg?alt=media&token=4ee45119-14af-47d1-8be4-87d39ac00372', 1, 1),
(4, 'santi', 'farioli', 'f@g.com', '$2a$11$wllI3Uf7fFecaH9C0kxaAuc3fqG66V5aZ65WGfpd//a', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fperro.jpg?alt=media&token=0888b8ee-6c0b-459a-9818-2f2ca60ccbf5', 1, 1),
(5, 'santi', 'farioli', 'santiago8773cba@gmail.com', '$2a$11$n00h3.km8Sys5OGzCaws5O/4DcEKIbyZJTOXx0JNBlV', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FCaptura%20de%20pantalla%202024-08-02%20181259.png?alt=media&token=7df91168-d187-4edd-b48a-c2c2e2479110', 1, 1),
(6, 'pepe', 'Super', 'pepe@gmail.com', 'NomvKRnteylT//HmchQUO0AxC36x1loKkgF/9XkkFTU=', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FMaid-Kuromi-Serving-Tea-Coloring-Page-For-Preschoolers.jpg?alt=media&token=425ed0d7-d4f8-41c1-a04b-51857c4c84c0', 1, 1),
(7, 'pepe2', 'Super2', 'pepe2@gmail.com', 'NomvKRnteylT//HmchQUO0AxC36x1loKkgF/9XkkFTU=', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fdescarga.png?alt=media&token=6a61cfb2-3e17-4291-a1f8-e99b9e93e429', 1, 1),
(8, 'SuperAdmin', 'Fermin', 'super@gaml.com', '$2a$11$T5gKZRQWwdACOTBd9j3XzOAxgRylptggpvLaNI0Cc8p', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FIMG_20220430_230650071_11zon.jpg?alt=media&token=f91e58d5-4815-4f69-aa64-446004c05f3f', 1, 1),
(9, 'Dios', 'Diocito', 'dios@gmail.com', '$2a$11$wJUEMwQ.6J7l0OInKY4yxein3ppm7odeUXMySvDz/08', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fdescarga.png?alt=media&token=6c451d28-3550-400c-b889-f775a431472d', 1, 1),
(10, 'Fermin2', 'Fernandez2', 'fermin20492@gmail.com', '$2a$11$IZYPotd9TUyfXi7DxZ9I9eIr0hsPOQ14TXbhdCNXgO7', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2F716cfe1d-166a-4a49-86fe-305673af145a.jfif?alt=media&token=22ed22af-c456-4fa6-be5d-b58e6ff53889', 1, 0),
(11, 'Fermin3', 'Fernandez3', 'fermin2049@gmail.com3', '$2a$11$veciKnl32GLJ2Jw8Qio11u8Wii/A3YU1INZcA7znX5d3Ip55RWCle', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2F5d97c491-8a5e-4c95-b00a-d207f84eb2ac.jfif?alt=media&token=44d62230-aa80-4f93-a2ae-399e156a23eb', 1, 1),
(12, 'Fermin4', 'Fernandez4', 'fermin2049@gmail.com4', '$2a$11$QrDzFIbnlqwU4R0N5I7sIux0gbRgsZZw3aXopBrzZ8PGxrOPpoXSK', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fperro%20argentino.jpg?alt=media&token=e5da2dfa-469f-428f-9f49-f590a3c63828', 1, 1),
(13, 'Fermin5', 'Fernandez5', 'fermin2049@gmail.com5', '$2a$11$z74M1KJI7HehBK8YfHIzIuVp5SsqHanHlOyJydTWw3cVDhu89wH5m', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fgigachad.jpg?alt=media&token=d81572b6-7397-4c86-a7dd-8a9338f09ac8', 1, 1),
(14, 'prueba', 'de contraseña', 'prueba01@gmail.com', '$2a$11$y0gs77hXSkaJYkAW/Uh5r.WohR.KGx.iF6ATrAPeEs.OLkWpcXAWC', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FRobloxScreenShot20221120_134524886.png?alt=media&token=388ef5c4-9eea-46ea-a601-265ae63b300b', 1, 1),
(15, 'Tomas1', 'Fernandez1', 'tomas1@gmail.com', '$2a$11$b5MxkaVP8s5a/FNSD6ZtyO/rI0rsLaRkMnxS4skRY3zUKag3zuHoW', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fball.jpg?alt=media&token=7bde1752-0e8b-4488-bf2f-9fabc17da81a', 2, 1),
(16, 'Alma Lucia ', 'Fernandez', 'alma1@gmail.com', '$2a$11$wS.GBf5uzOMnO.YN9kVyeeVPHd/4qIIMtD.5nuGnrrrYX/dsVAr9q', 'https://storage.googleapis.com/inmobilirianet.appspot.com/avatars/alma1@gmail.com/sonic.jpg', 1, 1),
(17, 'empleado', 'pinche', 'empleado@gmail.com', '$2a$11$c5ol87WfB0fTr8.gfT45ReUkHe.G.wRCTgXL6fqEnvdTQ6b1rjN4u', 'https://storage.googleapis.com/inmobilirianet.appspot.com/avatars/empleado@gmail.com/DALL·E 2024-08-23 11.23.18 - A visually striking image featuring a stylized star with a modern design in the center, using the colors of the United States flag—red, white', 2, 0),
(18, 'ppauchi', 'papuchi 02', 'papuchi@gmail.com', '$2a$11$lIzt4NeH4ZxU/2iu0heyCea9Hy9Zfuqp3MZChg3Ooa8SybF3eGaoW', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fpapuchi%40gmail.com%2Ferror.webp?alt=media&token=09c8469e-3d9d-48cb-8362-7a79f6cb41cb', 1, 0),
(19, 'papuchi', 'papucho', 'papuchoGmail.com', '$2a$11$5eCM6EjQSydiJ0VIHTefz.02pRTX6pOz1vgTnG2NJo3SI1OhUbm/e', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FpapuchoGmail.com%2F1.jpeg?alt=media&token=6def90b7-d44e-4400-b916-80f9692709d5', 1, 0),
(20, 'papauchi', 'papauchi', 'papauchi@gmail.com', '$2a$11$S0wAOQwR0YEsK0ft5676Euko9T1B.5419CaGvd7.JE74zfXvear1O', NULL, 2, 0),
(21, 'papuchi 2', 'papuchi', 'paupuchi@gmail.com', '$2a$11$cW6uK9POrAuB27PNkwsEIet5obHimYfBXPxcij81.7rfOUA0Vzrke', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fpaupuchi%40gmail.com%2Ficons8-google-web-search-100.png?alt=media&token=e7c9f716-6aa6-4863-baa8-6fff3974f703', 2, 0),
(24, 'Fermin', 'Fernandez', 'fermin20493@gmail.com', '$2a$11$ErFZ1x.Y/GUcp72alUhfguWNDUIkRA/kg7hOP6wmyxkVR22SpWAiu', NULL, 1, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`IdContrato`),
  ADD KEY `fk_inmueble` (`IdInmueble`),
  ADD KEY `fk_inquilino` (`IdInquilino`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`IdInmueble`),
  ADD KEY `fk_propietario` (`IdPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`IdInquilino`),
  ADD UNIQUE KEY `Dni` (`Dni`,`Telefono`,`Email`),
  ADD UNIQUE KEY `Dni_2` (`Dni`,`Telefono`,`Email`),
  ADD UNIQUE KEY `UQ_DNI` (`Dni`),
  ADD UNIQUE KEY `UQ_EMAIL` (`Email`),
  ADD UNIQUE KEY `UQ_TELEFONO` (`Telefono`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`IdPago`),
  ADD KEY `fk_contrato_pago` (`IdContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`IdPropietario`),
  ADD UNIQUE KEY `UQ_DNI` (`Dni`),
  ADD UNIQUE KEY `UQ_EMAIL` (`Email`),
  ADD UNIQUE KEY `UQ_TELEFONO` (`Telefono`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`IdUsuario`),
  ADD UNIQUE KEY `UQ_Usuario_Email` (`Email`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `IdContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `IdInmueble` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `IdInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `IdPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `IdPropietario` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `IdUsuario` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `fk_inmueble` FOREIGN KEY (`IdInmueble`) REFERENCES `inmueble` (`IdInmueble`),
  ADD CONSTRAINT `fk_inquilino` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilino` (`IdInquilino`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `fk_propietario` FOREIGN KEY (`IdPropietario`) REFERENCES `propietario` (`IdPropietario`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `fk_contrato_pago` FOREIGN KEY (`IdContrato`) REFERENCES `contrato` (`IdContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
