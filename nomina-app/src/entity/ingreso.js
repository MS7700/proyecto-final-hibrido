import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';

const Filters = [
    <NumberInput label="ID" source="id" />,
    <TextInput source="Nombre" />,
    <BooleanInput label="Automático" source="Automatico" />,
    <BooleanInput source="Porcentual" />,
    <NumberInput source="Cantidad" />,
    <BooleanInput source="Estado" />,
    
];

export const IngresoList = props => (
    <List {...props} filters={Filters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Nombre" />
            <BooleanField label="Automático" source="Automatico" />
            <BooleanField source="Porcentual" />
            <NumberField source="Cantidad" />
            <BooleanField source="Estado" />
        </Datagrid>
    </List>
);

export const IngresoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="Nombre" />
            <BooleanInput label="Automático" source="Automatico" />
            <BooleanInput source="Porcentual" />
            <NumberInput source="Cantidad" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Edit>
);

export const IngresoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Nombre" />
            <BooleanInput label="Automático" source="Automatico" />
            <BooleanInput source="Porcentual" />
            <NumberInput source="Cantidad" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Create>
);