import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput,NumberInput } from 'react-admin';

const Filters = [
    <NumberInput label="ID" source="id" />,
    <TextInput label="Tipo de Nómina"  source="Descripcion" />
];

export const TipoNominaList = props => (
    <List {...props} filters={Filters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField label="Tipo de Nómina" source="Descripcion" />
        </Datagrid>
    </List>
);

export const TipoNominaEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput label="Tipo de Nómina" source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const TipoNominaCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput label="Tipo de Nómina" source="Descripcion" />
        </SimpleForm>
    </Create>
);