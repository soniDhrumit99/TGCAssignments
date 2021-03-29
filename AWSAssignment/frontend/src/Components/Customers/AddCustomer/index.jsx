import { React, useState } from "react";
import { Form, Button, Col } from "react-bootstrap";
import toast from 'react-hot-toast';
import { withRouter } from 'react-router-dom';
import postCustomer from "./service";
import "./style.css";

function AddCustomer({ history }) {

  const [validated, setValidated] = useState(false);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [contact, setContact] = useState("");
  const [photo, setPhoto] = useState();
  const [fileLabel, setFileLabel] = useState('Profile Photo');

  const handleSubmit = (event) => {
    event.preventDefault();
    event.stopPropagation();
    const form = event.currentTarget;
    if (
      form.checkValidity() === false ||
      photo === undefined ||
      checkFileExtension() === false
    ) {
      console.log('Invalid');
    } else {
      toast
        .promise(
          postCustomer({ name, email, password, contact, photo }),
          {
            loading: "Adding customer",
            success: "Customer added successfully",
            error: (err) => "" + err,
          },
          {
            style: {
              minWidth: "300px",
              width: "500px",
              height: "50px",
              backgroundColor: "#FF9900",
              color: "#fff",
              fontSize: "1.2rem",
            },
          }
        )
        .then((resp) => {history.push("/")})
        .catch((err) => console.log(err.response.data));
    }
    setValidated(true);
  };

  const setFilename = (event) => {
    if(event.target.files[0] != null)
      setFileLabel(event.target.files[0].name);
    else
      setFileLabel('Profile Photo');
  }

  const checkFileExtension = () => {
    const validExtension = ['png', 'jpg', 'jpeg'];
    const ext = photo.type.split('/')[1];
    return validExtension.indexOf(ext) === -1 ? false : true;
  }

  return (
    <Form
      className="col-12"
      noValidate
      validated={validated}
      onSubmit={handleSubmit}
      id="add-cutomer-form"
    >
      <h4 className="mb-4 head">Add Customer</h4>
      <Form.Row className="my-2">
        <Form.Group as={Col} controlId="formName" className="px-3">
          <Form.Label>Name</Form.Label>
          <Form.Control
            required
            type="text"
            placeholder="Enter Name"
            onChange={(e) => setName(e.target.value)}
          />
          <Form.Control.Feedback type="invalid">
            Name is required
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group as={Col} controlId="formEmail" className="px-3">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            placeholder="Enter Email"
            required
            onChange={(e) => setEmail(e.target.value)}
          />
          <Form.Control.Feedback type="invalid">
            Invalid email format
          </Form.Control.Feedback>
        </Form.Group>
      </Form.Row>

      <Form.Row className="my-2">
        <Form.Group as={Col} controlId="formPassword" className="px-3">
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Password"
            required
            minLength="8"
            onChange={(e) => setPassword(e.target.value)}
          />
          <Form.Control.Feedback type="invalid">
            Password is a required field with a minimum length of 8.
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group as={Col} controlId="formConfirmPassword" className="px-3">
          <Form.Label>Confirm Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Password"
            required
            pattern={`^${password}$`}
            // onChange={(e) => setConfirmPassword(e.target.value)}
          />
          <Form.Control.Feedback type="invalid">
            Password does not match
          </Form.Control.Feedback>
        </Form.Group>
      </Form.Row>

      <Form.Row className="my-2">
        <Form.Group as={Col} controlId="formContactNumber" className="px-3">
          <Form.Label>Contact Number</Form.Label>
          <Form.Control
            type="tel"
            placeholder="Enter Contact Number"
            required
            pattern={`^[1-9]{1}[0-9]{9}$`}
            onChange={(e) => setContact(e.target.value)}
          />
          <Form.Control.Feedback type="invalid">
            Contact Number is a required field
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group as={Col} controlId="formProfilePhoto" className="mx-3">
          <Form.Label className="custom-file-label">
            {fileLabel.replaceAll('"', "")}
          </Form.Label>
          <Form.File
            placeholder="Choose a Profile photo"
            className="mt-1 custom-file-input mt-auto"
            required
            onChange={(e) => {
              setPhoto(e.target.files[0]);
              setFilename(e);
            }}
          />
          <Form.Control.Feedback type="invalid">
            Profile Photo is a required field
          </Form.Control.Feedback>
        </Form.Group>
      </Form.Row>

      <Form.Row className="d-flex justify-content-center align-items-center">
        <Button variant="success" type="submit" className="my-2 px-5 btn submit-btn">
          Submit
        </Button>
      </Form.Row>
    </Form>
  );
}

export default withRouter(AddCustomer);
