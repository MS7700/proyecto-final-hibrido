import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput,NumberInput } from 'react-admin';

const Filters = [
    <NumberInput label="ID" source="id" />,
    <TextInput label="Departamento"  source="Descripcion" />
];

export const DepartamentoList = props => (
    <List {...props} filters={Filters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField label="Departamento" source="Descripcion" />
        </Datagrid>
    </List>
);

export const DepartamentoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput label="Departamento" source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const DepartamentoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput label="Departamento" source="Descripcion" />
        </SimpleForm>
    </Create>
);