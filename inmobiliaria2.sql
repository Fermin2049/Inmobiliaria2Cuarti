-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-09-2024 a las 15:06:28
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
  `idContrato` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `MontoRenta` int(60) NOT NULL,
  `Deposito` int(50) NOT NULL,
  `Comision` int(50) NOT NULL,
  `Condiciones` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(10) NOT NULL,
  `idPropietario` int(10) NOT NULL,
  `Direccion` varchar(50) NOT NULL,
  `Uso` varchar(50) NOT NULL,
  `Tipo` varchar(50) NOT NULL,
  `CantAmbiente` int(10) NOT NULL,
  `Valor` int(50) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
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

INSERT INTO `inquilino` (`idInquilino`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Estado`) VALUES
(1, 'Fermin', 'Fernandez', 33539061, '2147483647', 'fermin2049@gmail.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `NroPago` int(10) NOT NULL,
  `FechaPago` date NOT NULL,
  `Detalle` varchar(50) NOT NULL,
  `Importe` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(10) NOT NULL,
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

INSERT INTO `propietario` (`idPropietario`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Estado`) VALUES
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
  `Id` int(10) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Contrasenia` varchar(50) NOT NULL,
  `Avatar` varchar(255) NOT NULL,
  `Rol` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`Id`, `Nombre`, `Apellido`, `Email`, `Contrasenia`, `Avatar`, `Rol`) VALUES
(1, 'Fermin', 'Fernandez', 'fermin2049@gmail.com', 'Fermin', '', '1'),
(2, 'Tomas', 'Fernandez', 'Tomas2049@gmail.com', 'asdsads', 'asdsadsa', '1'),
(3, 'ENRIQUE ROLANDO', 'GODOY', 'GODOY4695@GMAIL.COM', '$2a$11$wJYoIApbgt/JX/tsIXj2Q.3h3wUr.FDj3J17KyOZOQa', 'https://firebasestorage.googleapis.com/v0/b/inmobilirianet.appspot.com/o/avatars%2Fcat.jpg?alt=media&token=4ee45119-14af-47d1-8be4-87d39ac00372', '1');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `fk_inmueble` (`idInmueble`),
  ADD KEY `fk_inquilino` (`idInquilino`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `fk_propietario` (`idPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`),
  ADD UNIQUE KEY `Dni` (`Dni`,`Telefono`,`Email`),
  ADD UNIQUE KEY `Dni_2` (`Dni`,`Telefono`,`Email`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `fk_contrato_pago` (`idContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(10) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `Id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `fk_inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `fk_inquilino` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `fk_propietario` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `fk_contrato_pago` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
