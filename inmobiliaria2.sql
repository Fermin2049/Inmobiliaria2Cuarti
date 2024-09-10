-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 10-09-2024 a las 21:42:53
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
  `Condiciones` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`IdContrato`, `IdInmueble`, `IdInquilino`, `FechaInicio`, `FechaFin`, `MontoRenta`, `Deposito`, `Comision`, `Condiciones`) VALUES
(1, 2, 1, '2024-09-24', '2024-09-22', 12132, 2000, 25, '0'),
(2, 1, 1, '2024-09-05', '2024-10-12', 300000, 600000, 50000, 're nuevita'),
(6, 4, 4, '2024-09-01', '2024-09-30', 50000, 10000, 5000, 'ninguna'),
(7, 3, 5, '2024-09-01', '2024-11-30', 75000, 15000, 5000, 'rota ventena'),
(8, 1, 1, '2024-09-01', '2024-12-30', 100000, 50000, 5000, 'sanita');

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
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`IdInmueble`, `IdPropietario`, `Direccion`, `Uso`, `Tipo`, `CantAmbiente`, `Valor`, `Estado`) VALUES
(1, 1, '123 Calle Ficticia', 'Comercial', 0, 3, 150000, 1),
(2, 2, '456 Calle Imaginaria', 'Personal', 0, 5, 300000, 1),
(3, 1, 'Calle Falsa 123', 'Residencial', 0, 3, 150000, 1),
(4, 2, 'Avenida Siempre Viva 742', 'Comercial', 2, 1, 100000, 1);

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
(5, 'María', 'González', 87654321, '0987654321', 'maria.gonzalez@example.com', 1);

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
  `UsuarioAnulacion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`IdPago`, `IdContrato`, `NroPago`, `FechaPago`, `Detalle`, `Importe`, `Estado`, `UsuarioCreacion`, `UsuarioAnulacion`) VALUES
(1, 1, 1, '2024-09-08', 'devito', 205000.00, 1, '', ''),
(2, 2, 2, '2024-09-08', 'efectivo', 300000.00, 1, 'UsuarioCreacion', '');

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
(1, 'Juan', 'Pérez', 12345678, '555-1234', 'juan.perez@example.com', 1),
(2, 'María', 'González', 23456789, '555-5678', 'maria.gonzalez@example.com', 1),
(3, 'Carlos', 'Rodríguez', 34567890, '555-2345', 'carlos.rodriguez@example.com', 1),
(4, 'Ana', 'López', 45678901, '555-3456', 'ana.lopez@example.com', 1),
(5, 'Luis', 'Martínez', 56789012, '555-4567', 'luis.martinez@example.com', 1),
(6, 'Sofía', 'Gómez', 67890123, '555-5679', 'sofia.gomez@example.com', 1),
(7, 'Miguel', 'Fernández', 78901234, '555-6789', 'miguel.fernandez@example.com', 1),
(8, 'Laura', 'Hernández', 89012345, '555-7890', 'laura.hernandez@example.com', 1),
(9, 'Pedro', 'Jiménez', 90123456, '555-8901', 'pedro.jimenez@example.com', 1),
(10, 'Lucía', 'Díaz', 12309876, '555-9012', 'lucia.diaz@example.com', 1),
(11, 'Diego', 'Castro', 23409876, '555-0123', 'diego.castro@example.com', 1),
(12, 'Elena', 'Morales', 34509876, '555-3456', 'elena.morales@example.com', 1),
(13, 'Javier', 'Romero', 45609876, '555-4567', 'javier.romero@example.com', 1),
(14, 'Carmen', 'Navarro', 56709876, '555-5678', 'carmen.navarro@example.com', 1),
(15, 'Raúl', 'Ramos', 67809876, '555-6789', 'raul.ramos@example.com', 1),
(16, 'Natalia', 'Vega', 78909876, '555-7890', 'natalia.vega@example.com', 1),
(17, 'José', 'Molina', 89009876, '555-8901', 'jose.molina@example.com', 1),
(18, 'Clara', 'Ortega', 90109876, '555-9012', 'clara.ortega@example.com', 1),
(19, 'Andrés', 'Cruz', 23456780, '555-1230', 'andres.cruz@example.com', 1),
(20, 'Paula', 'Santos', 34567890, '555-2340', 'paula.santos@example.com', 1),
(21, 'ENRIQUE ROLANDO', 'GODOY', 78901234, '2664010204', 'GODOY4695@GMAIL.COM', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `IdUsuario` int(10) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Contrasenia` varchar(50) NOT NULL,
  `Avatar` varchar(255) NOT NULL,
  `Rol` varchar(50) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`IdUsuario`, `Nombre`, `Apellido`, `Email`, `Contrasenia`, `Avatar`, `Rol`, `Estado`) VALUES
(1, 'Fermin', 'Fernandez', 'fermin2049@gmail.com', 'Fermin', '', '1', 1),
(2, 'Tomas', 'Fernandez', 'Tomas2049@gmail.com', 'asdsads', 'asdsadsa', '1', 1),
(3, 'ENRIQUE ROLANDO', 'GODOY', 'GODOY4695@GMAIL.COM', '$2a$11$wJYoIApbgt/JX/tsIXj2Q.3h3wUr.FDj3J17KyOZOQa', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fcat.jpg?alt=media&token=4ee45119-14af-47d1-8be4-87d39ac00372', '1', 1),
(4, 'santi', 'farioli', 'f@g.com', '$2a$11$wllI3Uf7fFecaH9C0kxaAuc3fqG66V5aZ65WGfpd//a', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fperro.jpg?alt=media&token=0888b8ee-6c0b-459a-9818-2f2ca60ccbf5', '1', 1),
(5, 'santi', 'farioli', 'santiago8773cba@gmail.com', '$2a$11$n00h3.km8Sys5OGzCaws5O/4DcEKIbyZJTOXx0JNBlV', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2FCaptura%20de%20pantalla%202024-08-02%20181259.png?alt=media&token=7df91168-d187-4edd-b48a-c2c2e2479110', '1', 1);

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
  ADD UNIQUE KEY `Dni_2` (`Dni`,`Telefono`,`Email`);

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
  ADD PRIMARY KEY (`IdPropietario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`IdUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `IdContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `IdInmueble` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `IdInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `IdPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `IdPropietario` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `IdUsuario` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

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
