import * as React from "react";
import { List, Datagrid, TextField, Edit, Create,  SimpleForm, TextInput } from 'react-admin';

export const CuentaList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField label="Cuenta" source="Descripcion" />
        </Datagrid>
    </List>
);

export const CuentaEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="id" />
            <TextInput label="Cuenta" source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const CuentaCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="id" />
            <TextInput label="Cuenta" source="Descripcion" />
        </SimpleForm>
    </Create>
);