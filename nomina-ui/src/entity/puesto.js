import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberInput } from 'react-admin';


const postFilters = [
    <NumberInput label="ID" source="id" />,
    <TextInput label="Puesto"  source="Descripcion" />
];

export const PuestoList = props => (
    <List {...props} filters={postFilters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Descripcion" />
        </Datagrid>
    </List>
);

export const PuestoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const PuestoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Create>
);