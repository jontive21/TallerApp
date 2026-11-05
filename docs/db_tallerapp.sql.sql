-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 04-11-2026 a las 23:51:06
-- Versión del servidor: 10.11.16-MariaDB
-- Versión de PHP: 8.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_tallerapp`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `IdCliente` int(11) NOT NULL,
  `NombreCompleto` varchar(100) NOT NULL,
  `DNI` varchar(20) NOT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Direccion` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`IdCliente`, `NombreCompleto`, `DNI`, `Telefono`, `Email`, `Direccion`) VALUES
(1, 'María Rodríguez', '12345678', '11-4444-4444', 'maria@email.com', 'Av. San Martín 123'),
(2, 'Pedro Sánchez', '23456789', '11-5555-5555', 'pedro@email.com', 'Calle Belgrano 456'),
(3, 'Laura Martínez', '34567890', '11-6666-6666', 'laura@email.com', 'Av. Rivadavia 789'),
(4, 'Carlos Fernández', '40123456', '11-7001-1001', 'carlos.f@email.com', 'Av. Corrientes 100'),
(5, 'Lucía Gómez', '40234567', '11-7002-1002', 'lucia.g@email.com', 'Calle Florida 200'),
(6, 'Martín Díaz', '40345678', '11-7003-1003', 'martin.d@email.com', 'Av. Santa Fe 300'),
(7, 'Sofía López', '40456789', '11-7004-1004', 'sofia.l@email.com', 'Calle Lavalle 400'),
(8, 'Diego Torres', '40567890', '11-7005-1005', 'diego.t@email.com', 'Av. 9 de Julio 500'),
(9, 'Valentina Ruiz', '40678901', '11-7006-1006', 'valentina.r@email.com', 'Calle Maipú 600'),
(10, 'Santiago Morales', '40789012', '11-7007-1007', 'santiago.m@email.com', 'Av. Callao 700'),
(11, 'Camila Ortiz', '40890123', '11-7008-1008', 'camila.o@email.com', 'Calle Sarmiento 800'),
(12, 'Mateo Silva', '40901234', '11-7009-1009', 'mateo.s@email.com', 'Av. Pueyrredón 900'),
(13, 'Isabella Castro', '41012345', '11-7010-1010', 'isabella.c@email.com', 'Calle Rivadavia 1000'),
(14, 'Benjamín Vargas', '41123456', '11-7011-1011', 'benjamin.v@email.com', 'Av. Cabildo 1100'),
(15, 'Emma Romero', '41234567', '11-7012-1012', 'emma.r@email.com', 'Calle Juramento 1200'),
(16, 'Lucas Herrera', '41345678', '11-7013-1013', 'lucas.h@email.com', 'Av. Libertador 1300'),
(17, 'Mía Aguilar', '41456789', '11-7014-1014', 'mia.a@email.com', 'Calle Monroe 1400'),
(18, 'Thiago Mendoza', '41567890', '11-7015-1015', 'thiago.m@email.com', 'Av. del Libertador 1500'),
(19, 'Oliva Navarro', '41678901', '11-7016-1016', 'oliva.n@email.com', 'Calle Cabildo 1600'),
(20, 'Joaquín Sosa', '41789012', '11-7017-1017', 'joaquin.s@email.com', 'Av. Cabildo 1700'),
(21, 'Martina Rojas', '41890123', '11-7018-1018', 'martina.r@email.com', 'Calle Juramento 1800'),
(22, 'Emilio Cabrera', '41901234', '11-7019-1019', 'emilio.c@email.com', 'Av. Monroe 1900'),
(23, 'Renata Vega', '42012345', '11-7020-1020', 'renata.v@email.com', 'Calle Libertador 2000');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `comprobantespago`
--

CREATE TABLE `comprobantespago` (
  `IdComprobante` int(11) NOT NULL,
  `OrdenId` int(11) NOT NULL,
  `Monto` decimal(10,2) NOT NULL,
  `FechaPago` datetime NOT NULL,
  `Estado` varchar(20) NOT NULL DEFAULT 'Pagado',
  `Archivo` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mecanicos`
--

CREATE TABLE `mecanicos` (
  `IdMecanico` int(11) NOT NULL,
  `NombreCompleto` varchar(100) NOT NULL,
  `Matricula` varchar(20) NOT NULL,
  `Especialidad` varchar(50) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Telefono` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `mecanicos`
--

INSERT INTO `mecanicos` (`IdMecanico`, `NombreCompleto`, `Matricula`, `Especialidad`, `Email`, `Telefono`) VALUES
(1, 'Juan Pérez', 'MAT-001', 'Motor', 'juan@taller.com', '11-1111-1111'),
(2, 'Carlos López', 'MAT-002', 'Electricidad', 'carlos@taller.com', '11-2222-2222'),
(3, 'Ana García', 'MAT-003', 'Suspensión', 'ana@taller.com', '11-3333-3333'),
(4, 'Roberto Jiménez', 'MAT-004', 'Transmisión', 'roberto@taller.com', '11-4444-4444'),
(5, 'Fernando Castillo', 'MAT-005', 'Frenos', 'fernando@taller.com', '11-5555-5555'),
(6, 'Gabriel Mendoza', 'MAT-006', 'Motor', 'gabriel@taller.com', '11-6666-6666'),
(7, 'Alejandro Ríos', 'MAT-007', 'Electricidad', 'alejandro@taller.com', '11-7777-7777'),
(8, 'Nicolás Herrera', 'MAT-008', 'Suspensión', 'nicolas@taller.com', '11-8888-8888'),
(9, 'Sebastián Torres', 'MAT-009', 'Alineación', 'sebastian@taller.com', '11-9999-9999'),
(10, 'Diego Ramírez', 'MAT-010', 'Motor', 'diego.r@taller.com', '11-1010-1010'),
(11, 'Pablo Flores', 'MAT-011', 'Frenos', 'pablo@taller.com', '11-1111-1112'),
(12, 'Andrés Guerrero', 'MAT-012', 'Transmisión', 'andres@taller.com', '11-1212-1212'),
(13, 'Javier Medina', 'MAT-013', 'Electricidad', 'javier@taller.com', '11-1313-1313'),
(14, 'Ricardo Aguilar', 'MAT-014', 'Suspensión', 'ricardo@taller.com', '11-1414-1414'),
(15, 'Héctor Vargas', 'MAT-015', 'Alineación', 'hector@taller.com', '11-1515-1515');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenestrabajo`
--

CREATE TABLE `ordenestrabajo` (
  `IdOrden` int(11) NOT NULL,
  `VehiculoId` int(11) NOT NULL,
  `MecanicoId` int(11) NOT NULL,
  `FechaIngreso` datetime NOT NULL,
  `FechaEntregaEstimada` datetime DEFAULT NULL,
  `Estado` varchar(20) NOT NULL DEFAULT 'Pendiente',
  `DescripcionProblema` text DEFAULT NULL,
  `Observaciones` text DEFAULT NULL,
  `IdUsuarioCreacion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenrepuesto`
--

CREATE TABLE `ordenrepuesto` (
  `IdOrdenRepuesto` int(11) NOT NULL,
  `OrdenId` int(11) NOT NULL,
  `RepuestoId` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL,
  `PrecioUnitario` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `repuestos`
--

CREATE TABLE `repuestos` (
  `IdRepuesto` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Codigo` varchar(20) NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `Stock` int(11) NOT NULL DEFAULT 0,
  `Foto` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `repuestos`
--

INSERT INTO `repuestos` (`IdRepuesto`, `Nombre`, `Codigo`, `Precio`, `Stock`, `Foto`) VALUES
(1, 'Filtro de Aceite', 'REP-001', 2500.00, 50, NULL),
(2, 'Filtro de Aire', 'REP-002', 1800.00, 30, NULL),
(3, 'Pastillas de Freno', 'REP-003', 8500.00, 20, NULL),
(4, 'Aceite 5W30 1L', 'REP-004', 4200.00, 100, NULL),
(5, 'Bujías NGK (x4)', 'REP-005', 3500.00, 40, NULL),
(6, 'Correa de Distribución', 'REP-006', 12000.00, 15, NULL),
(7, 'Bomba de Agua', 'REP-007', 18500.00, 10, NULL),
(8, 'Radiador', 'REP-008', 45000.00, 5, NULL),
(9, 'Amortiguadores Delanteros (par)', 'REP-009', 28000.00, 8, NULL),
(10, 'Discos de Freno (par)', 'REP-010', 22000.00, 12, NULL),
(11, 'Zapatos de Freno', 'REP-011', 8500.00, 20, NULL),
(12, 'Kit de Embrague', 'REP-012', 35000.00, 6, NULL),
(13, 'Bomba de Combustible', 'REP-013', 15000.00, 10, NULL),
(14, 'Inyectores (x4)', 'REP-014', 24000.00, 8, NULL),
(15, 'Sensor de Oxígeno', 'REP-015', 9500.00, 15, NULL),
(16, 'Bobina de Encendido', 'REP-016', 7500.00, 20, NULL),
(17, 'Alternador', 'REP-017', 42000.00, 4, NULL),
(18, 'Motor de Arranque', 'REP-018', 38000.00, 5, NULL),
(19, 'Termostato', 'REP-019', 4500.00, 25, NULL),
(20, 'Manguera Superior', 'REP-020', 3200.00, 30, NULL),
(21, 'Banda Serpentina', 'REP-021', 6500.00, 18, NULL),
(22, 'Tensor de Correa', 'REP-022', 5500.00, 20, NULL),
(23, 'Filtro de Combustible', 'REP-023', 2800.00, 35, NULL),
(24, 'Filtro de Habitáculo', 'REP-024', 3500.00, 25, NULL),
(25, 'Aceite 10W40 1L', 'REP-025', 3800.00, 80, NULL),
(26, 'Líquido de Frenos DOT4 1L', 'REP-026', 2500.00, 50, NULL),
(27, 'Refrigerante 1L', 'REP-027', 2200.00, 60, NULL),
(28, 'Limpiaparabrisas (par)', 'REP-028', 4500.00, 40, NULL),
(29, 'Faro Delantero', 'REP-029', 15000.00, 10, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `IdUsuario` int(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `NombreCompleto` varchar(100) NOT NULL,
  `Rol` varchar(20) NOT NULL DEFAULT 'Empleado',
  `Avatar` varchar(255) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`IdUsuario`, `Email`, `PasswordHash`, `NombreCompleto`, `Rol`, `Avatar`, `FechaCreacion`) VALUES
(1, 'admin@tallerapp.com', '100000.p8JTouWrOs/cJSqwlSMvKg==.cqqZxoGera+MjaSgWgun8Ruug3cFFF2OFc9NSYFLspU=', 'Administrador', 'Admin', NULL, '2026-11-04 18:02:12'),
(2, 'empleado@tallerapp.com', '100000.p8JTouWrOs/cJSqwlSMvKg==.cqqZxoGera+MjaSgWgun8Ruug3cFFF2OFc9NSYFLspU=', 'Juan Empleado', 'Empleado', NULL, '2026-11-04 19:28:04');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vehiculos`
--

CREATE TABLE `vehiculos` (
  `IdVehiculo` int(11) NOT NULL,
  `Patente` varchar(10) NOT NULL,
  `Marca` varchar(50) NOT NULL,
  `Modelo` varchar(50) NOT NULL,
  `Anio` int(11) DEFAULT NULL,
  `Color` varchar(30) DEFAULT NULL,
  `ClienteId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `vehiculos`
--

INSERT INTO `vehiculos` (`IdVehiculo`, `Patente`, `Marca`, `Modelo`, `Anio`, `Color`, `ClienteId`) VALUES
(1, 'ABC123', 'Toyota', 'Corolla', 2020, 'Blanco', 1),
(2, 'DEF456', 'Ford', 'Focus', 2019, 'Negro', 2),
(3, 'GHI789', 'Volkswagen', 'Gol', 2021, 'Rojo', 3),
(4, 'JKL012', 'Chevrolet', 'Cruze', 2020, 'Gris', 4),
(5, 'MNO345', 'Fiat', 'Cronos', 2021, 'Blanco', 5),
(6, 'PQR678', 'Renault', 'Sandero', 2019, 'Rojo', 6),
(7, 'STU901', 'Peugeot', '208', 2022, 'Negro', 7),
(8, 'VWX234', 'Citroën', 'C3', 2020, 'Azul', 8),
(9, 'YZA567', 'Honda', 'Civic', 2021, 'Plata', 9),
(10, 'BCD890', 'Nissan', 'Sentra', 2020, 'Blanco', 10),
(11, 'EFG123', 'Hyundai', 'HB20', 2022, 'Rojo', 11),
(12, 'HIJ456', 'Kia', 'Rio', 2021, 'Gris', 12),
(13, 'KLM789', 'Suzuki', 'Swift', 2020, 'Negro', 13),
(14, 'NOP012', 'Mitsubishi', 'Lancer', 2019, 'Blanco', 14),
(15, 'QRS345', 'Subaru', 'Impreza', 2021, 'Azul', 15),
(16, 'TUV678', 'Mazda', '3', 2022, 'Rojo', 16),
(17, 'WXY901', 'Jeep', 'Renegade', 2020, 'Negro', 17),
(18, 'ZAB234', 'Fiat', 'Argo', 2021, 'Blanco', 18),
(19, 'CDE567', 'Renault', 'Kwid', 2022, 'Gris', 19),
(20, 'FGH890', 'Chevrolet', 'Onix', 2020, 'Plata', 20),
(21, 'IJK123', 'Volkswagen', 'Polo', 2021, 'Azul', 21),
(22, 'LMN456', 'Ford', 'Ka', 2020, 'Rojo', 22),
(23, 'OPQ789', 'Toyota', 'Hilux', 2022, 'Blanco', 23);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`IdCliente`),
  ADD UNIQUE KEY `DNI` (`DNI`);

--
-- Indices de la tabla `comprobantespago`
--
ALTER TABLE `comprobantespago`
  ADD PRIMARY KEY (`IdComprobante`),
  ADD KEY `OrdenId` (`OrdenId`);

--
-- Indices de la tabla `mecanicos`
--
ALTER TABLE `mecanicos`
  ADD PRIMARY KEY (`IdMecanico`),
  ADD UNIQUE KEY `Matricula` (`Matricula`);

--
-- Indices de la tabla `ordenestrabajo`
--
ALTER TABLE `ordenestrabajo`
  ADD PRIMARY KEY (`IdOrden`),
  ADD KEY `VehiculoId` (`VehiculoId`),
  ADD KEY `MecanicoId` (`MecanicoId`),
  ADD KEY `IdUsuarioCreacion` (`IdUsuarioCreacion`);

--
-- Indices de la tabla `ordenrepuesto`
--
ALTER TABLE `ordenrepuesto`
  ADD PRIMARY KEY (`IdOrdenRepuesto`),
  ADD KEY `OrdenId` (`OrdenId`),
  ADD KEY `RepuestoId` (`RepuestoId`);

--
-- Indices de la tabla `repuestos`
--
ALTER TABLE `repuestos`
  ADD PRIMARY KEY (`IdRepuesto`),
  ADD UNIQUE KEY `Codigo` (`Codigo`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`IdUsuario`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- Indices de la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  ADD PRIMARY KEY (`IdVehiculo`),
  ADD UNIQUE KEY `Patente` (`Patente`),
  ADD KEY `ClienteId` (`ClienteId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `IdCliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `comprobantespago`
--
ALTER TABLE `comprobantespago`
  MODIFY `IdComprobante` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `mecanicos`
--
ALTER TABLE `mecanicos`
  MODIFY `IdMecanico` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `ordenestrabajo`
--
ALTER TABLE `ordenestrabajo`
  MODIFY `IdOrden` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ordenrepuesto`
--
ALTER TABLE `ordenrepuesto`
  MODIFY `IdOrdenRepuesto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `repuestos`
--
ALTER TABLE `repuestos`
  MODIFY `IdRepuesto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IdUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  MODIFY `IdVehiculo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `comprobantespago`
--
ALTER TABLE `comprobantespago`
  ADD CONSTRAINT `comprobantespago_ibfk_1` FOREIGN KEY (`OrdenId`) REFERENCES `ordenestrabajo` (`IdOrden`);

--
-- Filtros para la tabla `ordenestrabajo`
--
ALTER TABLE `ordenestrabajo`
  ADD CONSTRAINT `ordenestrabajo_ibfk_1` FOREIGN KEY (`VehiculoId`) REFERENCES `vehiculos` (`IdVehiculo`),
  ADD CONSTRAINT `ordenestrabajo_ibfk_2` FOREIGN KEY (`MecanicoId`) REFERENCES `mecanicos` (`IdMecanico`),
  ADD CONSTRAINT `ordenestrabajo_ibfk_3` FOREIGN KEY (`IdUsuarioCreacion`) REFERENCES `usuarios` (`IdUsuario`);

--
-- Filtros para la tabla `ordenrepuesto`
--
ALTER TABLE `ordenrepuesto`
  ADD CONSTRAINT `ordenrepuesto_ibfk_1` FOREIGN KEY (`OrdenId`) REFERENCES `ordenestrabajo` (`IdOrden`),
  ADD CONSTRAINT `ordenrepuesto_ibfk_2` FOREIGN KEY (`RepuestoId`) REFERENCES `repuestos` (`IdRepuesto`);

--
-- Filtros para la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  ADD CONSTRAINT `vehiculos_ibfk_1` FOREIGN KEY (`ClienteId`) REFERENCES `clientes` (`IdCliente`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
