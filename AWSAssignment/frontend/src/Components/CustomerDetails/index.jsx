import { React, useEffect, useState } from 'react';
import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { useParams, withRouter } from 'react-router-dom';
import { getCustomersById, putCustomer } from './service';
import toast from 'react-hot-toast';
import './style.css';

function CustomerDetails() {

  let { id } = useParams();
  const [name, setName] = useState();
  const [email, setEmail] = useState();
  const [contact, setContact] = useState();
  const [photo, setPhoto] = useState();
  const [status, setStatus] = useState();

  const [validated, setValidated] = useState(false);
  const [notEditing, setnotEditing] = useState(true);


  useEffect(() => {
    getCustomersById(id)
      .then((resp) => {
        setName(resp.data.name);
        setEmail(resp.data.email);
        setContact(resp.data.contactnumber);
        setPhoto(resp.data.image);
        setStatus(resp.data.status);
        document.getElementById('loader').classList.add('hide');
      })
      .catch((err) => {
        console.log(err.response.data);
      });
  }, []);

  const handleSubmit = () => {
    if (
      name === '' ||
      email === '' ||
      contact === ''
    ) {
      console.log("Invalid");
    } else {
      toast
        .promise(
          putCustomer(id, { name, email, contact, status }),
          {
            loading: "Updating customer",
            success: "Customer updated successfully",
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
        .then((resp) => {
        })
        .catch((err) => console.log(err.response.data));
    }
  };

  const setEdit = (e) => {
    if(!notEditing){
      handleSubmit();
    }
    setnotEditing(!notEditing);
  };

  return (
    <Container fluid>
      <Row>
        <Col xs="12" className="text-right">
          <Button varient="warning" className="p-2 px-3 btn edit-btn" onClick={setEdit}>Edit</Button>
        </Col>
      </Row>
      <Row className="relative">
        <div className="loading" id="loader"><h2>Loading ...</h2></div>
        <Col
          lg="4"
          md="4"
          sm="12"
          className="text-md-right text-sm-center pr-5 my-3"
        >
          <img src={photo} alt="" height="200" width="200" />
        </Col>
        <Col lg="8" md="8" sm="12" className="bordered pl-4 my-3">
          <Form
            className="col-12"
            noValidate
            validated={validated}
            onSubmit={handleSubmit}
            id="add-customer-form"
          >
            <Form.Group as={Row} controlId="formName" className="px-1 mx-3 py-2">
              <Form.Label column sm="3">
                Name :
              </Form.Label>
              <Col sm="9">
                <Form.Control
                  required
                  type="text"
                  readOnly={notEditing}
                  plaintext={notEditing}
                  value={name}
                  placeholder="Enter Name"
                  onChange={(e) => setName(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Invalid Name
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formName" className="px-1 mx-3 py-2">
              <Form.Label column sm="3">
                Email :
              </Form.Label>
              <Col sm="9">
                <Form.Control
                  required
                  type="text"
                  readOnly={notEditing}
                  plaintext={notEditing}
                  value={email}
                  placeholder="Enter Name"
                  onChange={(e) => setEmail(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Invalid Email
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formName" className="px-1 mx-3 py-2">
              <Form.Label column sm="3">
                Contact :
              </Form.Label>
              <Col sm="9">
                <Form.Control
                  required
                  type="text"
                  readOnly={notEditing}
                  plaintext={notEditing}
                  value={contact}
                  placeholder="Enter Name"
                  onChange={(e) => setContact(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Invalid Contact
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formName" className="px-1 mx-3 py-2">
              <Form.Label column sm="3">
                Status :
              </Form.Label>
              <Col sm="3">
                <Form.Control
                  as="select"
                  custom
                  readOnly={notEditing}
                  disabled={notEditing}
                  required
                  value={status}
                  type="text"
                  placeholder="Enter Name"
                  onChange={(e) => setStatus(e.target.value)}
                >
                  <option value="0">Inactive</option>
                  <option value="1">Active</option>
                </Form.Control>
              </Col>
            </Form.Group>
          </Form>
        </Col>
      </Row>
    </Container>
  );
}

export default withRouter(CustomerDetails);
