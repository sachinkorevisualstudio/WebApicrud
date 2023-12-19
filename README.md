react js front end code

import React, { useState, useEffect } from 'react';
import axios from 'axios';

const StudentTable = () => {
  const [students, setStudents] = useState([]);
  const [newStudent, setNewStudent] = useState({ name: '' });
  const [editableStudent, setEditableStudent] = useState({ id: 0, name: '' });

  const handleInputChange = (e, type = 'new') => {
    const { name, value } = e.target;
    if (type === 'new') {
      setNewStudent((prev) => ({ ...prev, [name]: value }));
    } else {
      setEditableStudent((prev) => ({ ...prev, [name]: value }));
    }
  };

  const showAlert = (message) => {
    alert(message);
  };

  const handleAddStudent = () => {
    axios
      .post('https://localhost:7103/api/Students', newStudent)
      .then((response) => {
        setStudents([...students, response.data]);
        setNewStudent({ name: '' });
        showAlert('Student added successfully!');
      })
      .catch((error) => {
        console.error('Error adding student:', error);
        showAlert(`Error adding student: ${error.message}`);
      });
  };

  const handleEdit = (student) => {
    setEditableStudent({ ...student });
  };

  const handleUpdate = () => {
    const { id, name } = editableStudent;

    axios
      .put(`https://localhost:7103/api/Students/id?id=${id}`, { id, name })
      .then((response) => {
        setStudents(students.map((student) => (student.id === editableStudent.id ? response.data : student)));
        showAlert('Student updated successfully!');
      })
      .catch((error) => {
        console.error('Error updating student:', error);
        showAlert(`Error updating student: ${error.message}`);
      });
  };

  const handleDelete = (id) => {
    axios
      .delete(`https://localhost:7103/api/Students/id?id=${id}`)
      .then(() => {
        setStudents(students.filter((student) => student.id !== id));
        showAlert('Student deleted successfully!');
      })
      .catch((error) => {
        console.error('Error deleting student:', error);
        showAlert(`Error deleting student: ${error.message}`);
      });
  };

  useEffect(() => {
    axios
      .get('https://localhost:7103/api/Students')
      .then((response) => {
        setStudents(response.data);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
        showAlert(`Error fetching data: ${error.message}`);
      });
  }, []);

  return (
    <div>
      <h2>Student Data</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.id}>
              <td>{student.id}</td>
              <td>{student.name}</td>
              <td>
                <button onClick={() => handleEdit(student)}>Edit</button>
                <button onClick={() => handleDelete(student.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div>
        <h2>Add New Student</h2>
        <label>Name:</label>
        <input type="text" name="name" value={newStudent.name} onChange={(e) => handleInputChange(e, 'new')} />
        <button onClick={handleAddStudent}>Add Student</button>
      </div>

      <div>
        <h2>Edit Student</h2>
        <label>ID:</label>
        <input type="number" name="id" value={editableStudent.id} onChange={(e) => handleInputChange(e, 'edit')} disabled />
        <label>Name:</label>
        <input type="text" name="name" value={editableStudent.name} onChange={(e) => handleInputChange(e, 'edit')} />
        <button onClick={handleUpdate}>Update Student</button>
      </div>
    </div>
  );
};

export default StudentTable;
