import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberInput } from 'react-admin';


const Filters = [
    <NumberInput label="ID" source="id" />,
    <TextInput label="Puesto"  source="Descripcion" />
];

export const PuestoList = props => (
    <List {...props} filters={Filters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField label="Puesto"  source="Descripcion" />
        </Datagrid>
    </List>
);

export const PuestoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput label="Puesto"  source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const PuestoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput label="Puesto"  source="Descripcion" />
        </SimpleForm>
    </Create>
);