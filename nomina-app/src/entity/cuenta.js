import {
  List,
  Datagrid,
  TextField,
  Edit,
  Create,
  SimpleForm,
  TextInput,
} from "react-admin";

const Validations = (values) => {
  const errors = {};
  if (!values.id || typeof values.id !== "number") {
    errors.id = "El id es requerido.";
  }
  if (!values.Descripcion || values.Descripcion === "") {
    errors.Descripcion = "La descripción es requerida.";
  }
  return errors;
};

export const CuentaList = (props) => (
  <List {...props}>
    <Datagrid rowClick="edit">
      <TextField source="id" />
      <TextField label="Cuenta" source="Descripcion" />
    </Datagrid>
  </List>
);

export const CuentaEdit = (props) => (
  <Edit {...props}>
    <SimpleForm validate={Validations}>
      <TextInput source="id" validate={Validations} />
      <TextInput label="Cuenta" source="Descripcion" />
    </SimpleForm>
  </Edit>
);

export const CuentaCreate = (props) => (
  <Create {...props}>
    <SimpleForm validate={Validations}>
      <TextInput source="id" validate={Validations} />
      <TextInput label="Cuenta" source="Descripcion" />
    </SimpleForm>
  </Create>
);
