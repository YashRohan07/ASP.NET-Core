const apiBase = "https://localhost:7262/api/users";

const searchInput = document.getElementById('searchInput');
const statusFilter = document.getElementById('statusFilter');
const sortOrder = document.getElementById('sortOrder');
const searchBtn = document.getElementById('searchBtn');

const userForm = document.getElementById('userForm');
const userIdField = document.getElementById('userId');
const nameField = document.getElementById('name');
const emailField = document.getElementById('email');
const ageField = document.getElementById('age');
const addressField = document.getElementById('address');
const isActiveField = document.getElementById('isActive');

const userTableBody = document.getElementById('userTableBody');

// Load users when page loads and when search button is clicked
window.onload = loadUsers;
searchBtn.onclick = loadUsers;

async function loadUsers() {
    // Build API URL with search, filter, and sort query parameters
    const search = searchInput.value;
    const status = statusFilter.value;
    const sort = sortOrder.value;

    let url = `${apiBase}?search=${encodeURIComponent(search)}&status=${status}&sort=${sort}`;

    const res = await fetch(url);
    const data = await res.json();

    userTableBody.innerHTML = '';
    // Populate table rows dynamically from API response
    data.data.forEach(user => {
        const row = `
      <tr>
          <td>${user.id}</td>
          <td>${user.name}</td>
          <td>${user.email}</td>
          <td>${user.age}</td>
          <td>${user.address}</td>
          <td>${user.isActive ? 'Active' : 'Inactive'}</td>
          <td>
              <button onclick="editUser(${user.id})">Edit</button>
              <button onclick="deleteUser(${user.id})">Delete</button>
          </td>
      </tr>`;
        userTableBody.innerHTML += row;
    });
}

userForm.onsubmit = async function (e) {
    e.preventDefault();

    // Prepare user object from form inputs
    const id = userIdField.value;
    const user = {
        id: id ? parseInt(id) : 0,
        name: nameField.value,
        email: emailField.value,
        age: parseInt(ageField.value),
        address: addressField.value,
        isActive: isActiveField.checked
    };

    // Send PUT request if updating existing user, else POST for new user
    if (id) {
        await fetch(`${apiBase}/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });
    } else {
        await fetch(apiBase, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });
    }

    // Reset form and reload user list
    userForm.reset();
    userIdField.value = '';
    document.getElementById('formTitle').innerText = 'Add User';
    loadUsers();
};

async function editUser(id) {
    // Fetch user data by ID and populate form fields for editing
    const res = await fetch(`${apiBase}/${id}`);
    const data = await res.json();
    const user = data.data;

    userIdField.value = user.id;
    nameField.value = user.name;
    emailField.value = user.email;
    ageField.value = user.age;
    addressField.value = user.address;
    isActiveField.checked = user.isActive;

    document.getElementById('formTitle').innerText = 'Update User';
}

async function deleteUser(id) {
    // Confirm deletion and call API to delete user by ID
    if (confirm('Are you sure you want to delete this user?')) {
        await fetch(`${apiBase}/${id}`, { method: 'DELETE' });
        loadUsers();
    }
}
