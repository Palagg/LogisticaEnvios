// Configuración de la API
const API_BASE_URL = 'https://localhost:7103/api'; // Cambia el puerto según tu configuración

// Función para mostrar notificaciones
function showNotification(message, type = 'success') {
    const notification = document.getElementById('notification');
    notification.textContent = message;
    notification.className = `notification ${type} show`;
    
    setTimeout(() => {
        notification.classList.remove('show');
    }, 3000);
}

// Función para cambiar de tab
function showTab(tabName) {
    // Ocultar todos los tabs
    const tabs = document.querySelectorAll('.tab-content');
    tabs.forEach(tab => tab.classList.remove('active'));
    
    // Desactivar todos los botones
    const btns = document.querySelectorAll('.tab-btn');
    btns.forEach(btn => btn.classList.remove('active'));
    
    // Mostrar el tab seleccionado
    document.getElementById(tabName).classList.add('active');
    event.target.classList.add('active');
    
    // Cargar datos según el tab
    switch(tabName) {
        case 'clientes':
            loadClientes();
            break;
        case 'tipo-producto':
            loadTiposProducto();
            break;
        case 'plan-entrega':
            loadPlanesEntrega();
            break;
        case 'envios-terrestres':
            loadEnviosTerrestres();
            break;
        case 'envios-maritimos':
            loadEnviosMaritimos();
            break;
        case 'bodegas':
            loadBodegas();
            break;
        case 'puertos':
            loadPuertos();
            break;
    }
}

// ===== CLIENTES =====
async function loadClientes() {
    try {
        const response = await fetch(`${API_BASE_URL}/Clientes`);
        const clientes = await response.json();
        
        const list = document.getElementById('clientesList');
        
        if (clientes.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay clientes registrados</p></div>';
            return;
        }
        
        list.innerHTML = clientes.map(cliente => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>Cédula:</strong> ${cliente.cedula}</p>
                    <p><strong>Nombre:</strong> ${cliente.nombre}</p>
                    <p><strong>Dirección:</strong> ${cliente.direccion || 'N/A'}</p>
                    <p><strong>Email:</strong> ${cliente.email || 'N/A'}</p>
                </div>
                <button class="btn btn-danger" onclick="deleteCliente('${cliente.cedula}')">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar clientes: ' + error.message, 'error');
    }
}

document.getElementById('clienteForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const cliente = {
        cedula: document.getElementById('clienteCedula').value,
        nombre: document.getElementById('clienteNombre').value,
        direccion: document.getElementById('clienteDireccion').value,
        email: document.getElementById('clienteEmail').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/Clientes`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(cliente)
        });
        
        if (response.ok) {
            showNotification('Cliente agregado exitosamente');
            e.target.reset();
            loadClientes();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar cliente: ' + error.message, 'error');
    }
});

async function deleteCliente(cedula) {
    if (!confirm('¿Está seguro de eliminar este cliente?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/Clientes/${cedula}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Cliente eliminado exitosamente');
            loadClientes();
        } else {
            showNotification('Error al eliminar cliente', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== TIPOS DE PRODUCTO =====
async function loadTiposProducto() {
    try {
        const response = await fetch(`${API_BASE_URL}/TipoProducto`);
        
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        
        const text = await response.text();
        const tipos = text ? JSON.parse(text) : [];
        
        const list = document.getElementById('tiposProductoList');
        
        if (tipos.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay tipos de producto registrados</p></div>';
            return;
        }
        
        list.innerHTML = tipos.map(tipo => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${tipo.tipoProductoID}</p>
                    <p><strong>Nombre:</strong> ${tipo.nombre}</p>
                    <p><strong>Descripción:</strong> ${tipo.descripcion || 'N/A'}</p>
                </div>
                <button class="btn btn-danger" onclick="deleteTipoProducto(${tipo.tipoProductoID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar tipos de producto: ' + error.message, 'error');
    }
}

document.getElementById('tipoProductoForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const tipo = {
        nombre: document.getElementById('tipoProductoNombre').value,
        descripcion: document.getElementById('tipoProductoDescripcion').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/TipoProducto`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(tipo)
        });
        
        if (response.ok) {
            showNotification('Tipo de producto agregado exitosamente');
            e.target.reset();
            loadTiposProducto();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar tipo de producto: ' + error.message, 'error');
    }
});

async function deleteTipoProducto(id) {
    if (!confirm('¿Está seguro de eliminar este tipo de producto?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/TipoProducto/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Tipo de producto eliminado exitosamente');
            loadTiposProducto();
        } else {
            showNotification('Error al eliminar tipo de producto', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== FUNCIONES AUXILIARES PARA CARGAR DROPDOWNS =====
async function loadTipoProductoOptions() {
    try {
        const response = await fetch(`${API_BASE_URL}/TipoProducto`);
        const tipos = await response.json();
        
        const select = document.getElementById('planTipoProductoID');
        select.innerHTML = '<option value="">Seleccione un tipo de producto</option>';
        tipos.forEach(tipo => {
            select.innerHTML += `<option value="${tipo.tipoProductoID}">${tipo.nombre} - ${tipo.descripcion}</option>`;
        });
    } catch (error) {
        console.error('Error cargando tipos de producto:', error);
    }
}

async function loadClienteOptions() {
    try {
        const response = await fetch(`${API_BASE_URL}/Clientes`);
        const clientes = await response.json();
        
        const select = document.getElementById('planCedulaCliente');
        select.innerHTML = '<option value="">Seleccione un cliente</option>';
        clientes.forEach(cliente => {
            select.innerHTML += `<option value="${cliente.cedula}">${cliente.nombre} (${cliente.cedula})</option>`;
        });
    } catch (error) {
        console.error('Error cargando clientes:', error);
    }
}

async function loadBodegaOptions() {
    try {
        const response = await fetch(`${API_BASE_URL}/Bodega`);
        const bodegas = await response.json();
        
        const select = document.getElementById('bodegaEntregaID');
        select.innerHTML = '<option value="">Seleccione una bodega</option>';
        bodegas.forEach(bodega => {
            select.innerHTML += `<option value="${bodega.bodegaID}">${bodega.ubicacion} (Capacidad: ${bodega.capacidad})</option>`;
        });
    } catch (error) {
        console.error('Error cargando bodegas:', error);
    }
}

async function loadPuertoOptions() {
    try {
        const response = await fetch(`${API_BASE_URL}/Puerto`);
        const puertos = await response.json();
        
        const select = document.getElementById('puertoEntregaID');
        select.innerHTML = '<option value="">Seleccione un puerto</option>';
        puertos.forEach(puerto => {
            select.innerHTML += `<option value="${puerto.puertoID}">${puerto.nombre} - ${puerto.ubicacion}</option>`;
        });
    } catch (error) {
        console.error('Error cargando puertos:', error);
    }
}

async function loadPlanEntregaOptions() {
    try {
        const response = await fetch(`${API_BASE_URL}/PlanDeEntrega`);
        const text = await response.text();
        const planes = text ? JSON.parse(text) : [];
        
        // Para envíos terrestres
        const selectTerrestre = document.getElementById('planIDTerrestre');
        if (selectTerrestre) {
            selectTerrestre.innerHTML = '<option value="">Seleccione un plan de entrega</option>';
            planes.forEach(plan => {
                selectTerrestre.innerHTML += `<option value="${plan.planID}">Plan #${plan.planID} - Guía: ${plan.numeroGuia}</option>`;
            });
        }
        
        // Para envíos marítimos
        const selectMaritimo = document.getElementById('planIDMaritimo');
        if (selectMaritimo) {
            selectMaritimo.innerHTML = '<option value="">Seleccione un plan de entrega</option>';
            planes.forEach(plan => {
                selectMaritimo.innerHTML += `<option value="${plan.planID}">Plan #${plan.planID} - Guía: ${plan.numeroGuia}</option>`;
            });
        }
    } catch (error) {
        console.error('Error cargando planes de entrega:', error);
    }
}

// ===== PLANES DE ENTREGA =====
async function loadPlanesEntrega() {
    // Cargar opciones de los dropdowns
    await loadTipoProductoOptions();
    await loadClienteOptions();
    
    try {
        const response = await fetch(`${API_BASE_URL}/PlanDeEntrega`);
        
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        
        const text = await response.text();
        const planes = text ? JSON.parse(text) : [];
        
        const list = document.getElementById('planesEntregaList');
        
        if (planes.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay planes de entrega registrados</p></div>';
            return;
        }
        
        list.innerHTML = planes.map(plan => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${plan.planID}</p>
                    <p><strong>Tipo Producto ID:</strong> ${plan.tipoProductoID}</p>
                    <p><strong>Cantidad:</strong> ${plan.cantidad}</p>
                    <p><strong>Fecha Registro:</strong> ${new Date(plan.fechaRegistro).toLocaleDateString()}</p>
                    <p><strong>Fecha Entrega:</strong> ${new Date(plan.fechaEntrega).toLocaleDateString()}</p>
                    <p><strong>Precio Envío:</strong> $${plan.precioEnvio}</p>
                    <p><strong>Número Guía:</strong> ${plan.numeroGuia}</p>
                    <p><strong>Cliente:</strong> ${plan.clienteCedula}</p>
                </div>
                <button class="btn btn-danger" onclick="deletePlanEntrega(${plan.planID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar planes de entrega: ' + error.message, 'error');
    }
}

document.getElementById('planEntregaForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const plan = {
        tipoProductoID: parseInt(document.getElementById('planTipoProductoID').value),
        cantidad: parseInt(document.getElementById('planCantidad').value),
        fechaRegistro: document.getElementById('planFechaRegistro').value,
        fechaEntrega: document.getElementById('planFechaEntrega').value,
        precioEnvio: parseFloat(document.getElementById('planPrecioEnvio').value),
        numeroGuia: parseInt(document.getElementById('planNumeroGuia').value),
        clienteCedula: document.getElementById('planCedulaCliente').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/PlanDeEntrega`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(plan)
        });
        
        if (response.ok) {
            showNotification('Plan de entrega agregado exitosamente');
            e.target.reset();
            loadPlanesEntrega();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar plan de entrega: ' + error.message, 'error');
    }
});

async function deletePlanEntrega(id) {
    if (!confirm('¿Está seguro de eliminar este plan de entrega?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/PlanDeEntrega/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Plan de entrega eliminado exitosamente');
            loadPlanesEntrega();
        } else {
            showNotification('Error al eliminar plan de entrega', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== ENVÍOS TERRESTRES =====
async function loadEnviosTerrestres() {
    // Cargar opciones de los dropdowns
    await loadBodegaOptions();
    await loadPlanEntregaOptions();
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioTerrestre`);
        
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        
        const text = await response.text();
        const envios = text ? JSON.parse(text) : [];
        
        const list = document.getElementById('enviosTerrestresList');
        
        if (envios.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay envíos terrestres registrados</p></div>';
            return;
        }
        
        list.innerHTML = envios.map(envio => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${envio.envioTerrestreID}</p>
                    <p><strong>Placa:</strong> ${envio.placaVehiculo}</p>
                    <p><strong>Bodega ID:</strong> ${envio.bodegaEntregaID}</p>
                    <p><strong>Plan ID:</strong> ${envio.planID}</p>
                </div>
                <button class="btn btn-danger" onclick="deleteEnvioTerrestre(${envio.envioTerrestreID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar envíos terrestres: ' + error.message, 'error');
    }
}

document.getElementById('envioTerrestreForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const envio = {
        placaVehiculo: document.getElementById('placaVehiculo').value,
        bodegaEntregaID: parseInt(document.getElementById('bodegaEntregaID').value),
        planID: parseInt(document.getElementById('planIDTerrestre').value)
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioTerrestre`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(envio)
        });
        
        if (response.ok) {
            showNotification('Envío terrestre agregado exitosamente');
            e.target.reset();
            loadEnviosTerrestres();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar envío: ' + error.message, 'error');
    }
});

async function deleteEnvioTerrestre(id) {
    if (!confirm('¿Está seguro de eliminar este envío?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioTerrestre/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Envío eliminado exitosamente');
            loadEnviosTerrestres();
        } else {
            showNotification('Error al eliminar envío', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== ENVÍOS MARÍTIMOS =====
async function loadEnviosMaritimos() {
    // Cargar opciones de los dropdowns
    await loadPuertoOptions();
    await loadPlanEntregaOptions();
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioMaritimo`);
        
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        
        const text = await response.text();
        const envios = text ? JSON.parse(text) : [];
        
        const list = document.getElementById('enviosMaritimosList');
        
        if (envios.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay envíos marítimos registrados</p></div>';
            return;
        }
        
        list.innerHTML = envios.map(envio => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${envio.envioMaritimoID}</p>
                    <p><strong>Número de Flota:</strong> ${envio.numeroFlota}</p>
                    <p><strong>Puerto:</strong> ${envio.puertoEntregaID}</p>
                    <p><strong>Plan:</strong> ${envio.planID}</p>
                </div>
                <button class="btn btn-danger" onclick="deleteEnvioMaritimo(${envio.envioMaritimoID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar envíos marítimos: ' + error.message, 'error');
    }
}

document.getElementById('envioMaritimoForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const envio = {
        numeroFlota: document.getElementById('numeroFlota').value,
        puertoEntregaID: parseInt(document.getElementById('puertoEntregaID').value),
        planID: parseInt(document.getElementById('planIDMaritimo').value)
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioMaritimo`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(envio)
        });
        
        if (response.ok) {
            showNotification('Envío marítimo agregado exitosamente');
            e.target.reset();
            loadEnviosMaritimos();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar envío: ' + error.message, 'error');
    }
});

async function deleteEnvioMaritimo(id) {
    if (!confirm('¿Está seguro de eliminar este envío?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/EnvioMaritimo/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Envío eliminado exitosamente');
            loadEnviosMaritimos();
        } else {
            showNotification('Error al eliminar envío', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== BODEGAS =====
async function loadBodegas() {
    try {
        const response = await fetch(`${API_BASE_URL}/Bodega`);
        const bodegas = await response.json();
        
        const list = document.getElementById('bodegasList');
        
        if (bodegas.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay bodegas registradas</p></div>';
            return;
        }
        
        list.innerHTML = bodegas.map(bodega => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${bodega.bodegaID}</p>
                    <p><strong>Ubicación:</strong> ${bodega.ubicacion}</p>
                    <p><strong>Capacidad:</strong> ${bodega.capacidad}</p>
                </div>
                <button class="btn btn-danger" onclick="deleteBodega(${bodega.bodegaID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar bodegas: ' + error.message, 'error');
    }
}

document.getElementById('bodegaForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const bodega = {
        ubicacion: document.getElementById('bodegaUbicacion').value,
        capacidad: parseInt(document.getElementById('bodegaCapacidad').value)
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/Bodega`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(bodega)
        });
        
        if (response.ok) {
            showNotification('Bodega agregada exitosamente');
            e.target.reset();
            loadBodegas();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar bodega: ' + error.message, 'error');
    }
});

async function deleteBodega(id) {
    if (!confirm('¿Está seguro de eliminar esta bodega?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/Bodega/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Bodega eliminada exitosamente');
            loadBodegas();
        } else {
            showNotification('Error al eliminar bodega', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// ===== PUERTOS =====
async function loadPuertos() {
    try {
        const response = await fetch(`${API_BASE_URL}/Puerto`);
        const puertos = await response.json();
        
        const list = document.getElementById('puertosList');
        
        if (puertos.length === 0) {
            list.innerHTML = '<div class="empty-state"><p>No hay puertos registrados</p></div>';
            return;
        }
        
        list.innerHTML = puertos.map(puerto => `
            <div class="data-item">
                <div class="data-item-info">
                    <p><strong>ID:</strong> ${puerto.puertoID}</p>
                    <p><strong>Nombre:</strong> ${puerto.nombre}</p>
                    <p><strong>Ubicación:</strong> ${puerto.ubicacion || 'N/A'}</p>
                </div>
                <button class="btn btn-danger" onclick="deletePuerto(${puerto.puertoID})">Eliminar</button>
            </div>
        `).join('');
    } catch (error) {
        showNotification('Error al cargar puertos: ' + error.message, 'error');
    }
}

document.getElementById('puertoForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const puerto = {
        nombre: document.getElementById('puertoNombre').value,
        ubicacion: document.getElementById('puertoUbicacion').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/Puerto`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(puerto)
        });
        
        if (response.ok) {
            showNotification('Puerto agregado exitosamente');
            e.target.reset();
            loadPuertos();
        } else {
            const error = await response.text();
            showNotification('Error: ' + error, 'error');
        }
    } catch (error) {
        showNotification('Error al agregar puerto: ' + error.message, 'error');
    }
});

async function deletePuerto(id) {
    if (!confirm('¿Está seguro de eliminar este puerto?')) return;
    
    try {
        const response = await fetch(`${API_BASE_URL}/Puerto/${id}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            showNotification('Puerto eliminado exitosamente');
            loadPuertos();
        } else {
            showNotification('Error al eliminar puerto', 'error');
        }
    } catch (error) {
        showNotification('Error: ' + error.message, 'error');
    }
}

// Cargar datos iniciales
loadClientes();
