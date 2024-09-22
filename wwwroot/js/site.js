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
});
