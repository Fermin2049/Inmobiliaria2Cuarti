window.addEventListener('DOMContentLoaded', (event) => {
	// Toggle the side navigation
	const sidebarToggle = document.body.querySelector('#sidebarToggle');
	if (sidebarToggle) {
		sidebarToggle.addEventListener('click', (event) => {
			event.preventDefault();
			document.body.classList.toggle('sb-sidenav-toggled');
			localStorage.setItem(
				'sb|sidebar-toggle',
				document.body.classList.contains('sb-sidenav-toggled')
			);
		});
	}

	// Confirm delete function
	document.querySelectorAll('[data-delete-url]').forEach((button) => {
		button.addEventListener('click', (event) => {
			event.preventDefault();
			const url = button.getAttribute('data-delete-url');
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡No podrás revertir esto!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, eliminarlo!',
			}).then((result) => {
				if (result.isConfirmed) {
					window.location.href = url;
				}
			});
		});
	});

	// Success message function
	const successMessage = document.body.getAttribute('data-success-message');
	if (successMessage) {
		Swal.fire({
			position: 'top-end',
			icon: 'success',
			title: successMessage,
			showConfirmButton: false,
			timer: 1500,
		});
	}

	// Error message function
	const errorMessage = document.body.getAttribute('data-error-message');
	if (errorMessage) {
		Swal.fire({
			icon: 'error',
			title: 'Error',
			text: errorMessage,
			confirmButtonText: 'OK',
		});
	}

	// SweetAlert for forms
	document
		.getElementById('uploadAvatarForm')
		.addEventListener('submit', function (event) {
			event.preventDefault();
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡Subirás un nuevo avatar!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, subir',
			}).then((result) => {
				if (result.isConfirmed) {
					this.submit();
				}
			});
		});

	document
		.getElementById('deleteAvatarForm')
		.addEventListener('submit', function (event) {
			event.preventDefault();
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡Eliminarás tu avatar actual!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, eliminar',
			}).then((result) => {
				if (result.isConfirmed) {
					this.submit();
				}
			});
		});

	document.querySelectorAll('.selectAvatarForm').forEach((form) => {
		form.addEventListener('submit', function (event) {
			event.preventDefault();
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡Seleccionarás este avatar!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, seleccionar',
			}).then((result) => {
				if (result.isConfirmed) {
					this.submit();
				}
			});
		});
	});

	document.querySelectorAll('.deletePhotoForm').forEach((form) => {
		form.addEventListener('submit', function (event) {
			event.preventDefault();
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡Eliminarás esta foto!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, eliminar',
			}).then((result) => {
				if (result.isConfirmed) {
					this.submit();
				}
			});
		});
	});

	document
		.getElementById('configurarPerfilForm')
		.addEventListener('submit', function (event) {
			event.preventDefault();
			Swal.fire({
				title: '¿Estás seguro?',
				text: '¡Actualizarás tu perfil!',
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Sí, actualizar',
			}).then((result) => {
				if (result.isConfirmed) {
					this.submit();
				}
			});
		});

	// Gráficos de Chart.js
	var tenantDistributionCtx = document
		.getElementById('tenantDistributionChart')
		.getContext('2d');
	var tenantDistributionChart = new Chart(tenantDistributionCtx, {
		type: 'pie',
		data: {
			labels: ['Inmueble 1', 'Inmueble 2', 'Inmueble 3'], // Ajusta las etiquetas según tus datos
			datasets: [
				{
					data: JSON.parse(
						document.getElementById('tenantDistributionChart').dataset.values
					),
					backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
				},
			],
		},
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: 'top',
				},
				title: {
					display: true,
					text: 'Distribución de Inquilinos por Inmueble',
				},
			},
		},
	});

	// Inicializar gráfico de pagos mensuales
	var monthlyPaymentsCtx = document
		.getElementById('monthlyPaymentsChart')
		.getContext('2d');
	var monthlyPaymentsChart = new Chart(monthlyPaymentsCtx, {
		type: 'bar',
		data: {
			labels: [
				'Enero',
				'Febrero',
				'Marzo',
				'Abril',
				'Mayo',
				'Junio',
				'Julio',
				'Agosto',
				'Septiembre',
				'Octubre',
				'Noviembre',
				'Diciembre',
			],
			datasets: [
				{
					label: 'Pagos Mensuales',
					data: JSON.parse(
						document.getElementById('monthlyPaymentsChart').dataset.values
					),
					backgroundColor: '#36A2EB',
				},
			],
		},
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: 'top',
				},
				title: {
					display: true,
					text: 'Pagos Recibidos Mensualmente',
				},
			},
		},
	});

	// Inicializar gráfico de nuevos contratos por mes
	var newContractsCtx = document
		.getElementById('newContractsChart')
		.getContext('2d');
	var newContractsChart = new Chart(newContractsCtx, {
		type: 'bar',
		data: {
			labels: [
				'Enero',
				'Febrero',
				'Marzo',
				'Abril',
				'Mayo',
				'Junio',
				'Julio',
				'Agosto',
				'Septiembre',
				'Octubre',
				'Noviembre',
				'Diciembre',
			],
			datasets: [
				{
					label: 'Nuevos Contratos',
					data: JSON.parse(
						document.getElementById('newContractsChart').dataset.values
					),
					backgroundColor: '#FF6384',
				},
			],
		},
		options: {
			responsive: true,
			plugins: {
				legend: {
					position: 'top',
				},
				title: {
					display: true,
					text: 'Nuevos Contratos por Mes',
				},
			},
		},
	});
});
