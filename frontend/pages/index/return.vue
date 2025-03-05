<template>
    <div class="p-6 max-w-lg mx-auto">
        <h1 class="text-2xl font-semibold mb-4">Return Car</h1>

        <UCard class="p-4">
            <!-- Booking Number Input & Fetch Button in One Row -->
            <div class="mb-4 flex gap-4">
                <UInput v-model="bookingNumber" placeholder="Enter Booking Number" class="flex-1" />
                <UButton :loading="loading" :disabled="!bookingNumber || initialized" @click="initializeReturn">
                    Load Booking Data
                </UButton>
            </div>

            <!-- Read-Only Booking Data -->
            <UForm :state="booking">
                <div class="mb-4">
                    <UInput v-model="booking.registrationNumber" label="Registration Number" disabled />
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.customerIdentification" label="Customer ID" disabled />
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.date" label="Handout Date" type="datetime-local" disabled />
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.odometer" label="Odometer at Handout" disabled />
                </div>

                <!-- Editable Fields for Return -->
                <div class="mb-4">
                    <UInput v-model="booking.returnDate" label="Return Date" type="datetime-local">
                        <template #trailing>
                            <UButton size="xs" @click="setReturnDateNow">Now</UButton>
                        </template>
                    </UInput>
                </div>

                <div class="mb-4">
                    <UInput placeholder="Return Odometer" v-model="booking.returnOdometer" label="Return Odometer"
                        :class="{ 'border-red-500': hasInvalidChars }" @input="validateReturnOdometer" />
                </div>
            </UForm>
            <div class="flex gap-4 mt-4">
                <UButton color="primary" :loading="submitting"
                    :disabled="!initialized || !booking.returnDate || !booking.returnOdometer" @click="submitReturn"
                    class="flex-1">
                    Return
                </UButton>

                <UButton color="red" @click="clearForm" class="flex-1">
                    Clear
                </UButton>
            </div>
            <!-- Display Calculated Price if available -->
            <div v-if="booking.calculatedPrice !== null" class="mt-4 p-4 bg-gray-100 rounded">
                <h2 class="text-lg font-semibold">Total Rental Cost</h2>
                <p class="text-xl font-bold">€{{ booking.calculatedPrice }}</p>
            </div>
        </UCard>
    </div>
</template>

<script setup>
import { ref } from "vue";

const bookingNumber = ref(""); // Input field for Booking Number
const booking = ref({
    registrationNumber: "",
    customerIdentification: "",
    date: "",
    odometer: "",
    returnDate: "",
    returnOdometer: "",
    calculatedPrice: null
});

const loading = ref(false);
const initialized = ref(false);
const hasInvalidChars = ref(false);

const initializeReturn = async () => {
    if (!bookingNumber.value) return; // Prevent fetching if empty
    loading.value = true;

    try {
        const response = await fetch(`http://localhost:5270/bookings/${bookingNumber.value}`, { method: "GET" });
        const data = await response.json();

        if (!data.handOutDateUtc) {
            initialized.value = false;
            return;
        }

        const formattedDate = new Date(data.handOutDateUtc).toISOString().slice(0, 16);

        const formattedReturnDate = !data.returnDateUtc ? "" : new Date(data.returnDateUtc).toISOString().slice(0, 16);

        // Populate form with read-only data
        booking.value = {
            registrationNumber: data.registrationNumber,
            customerIdentification: data.cusomterIdentifcation,
            type: data.type,
            date: formattedDate,
            odometer: data.odometer,
            returnDate: formattedReturnDate,
            returnOdometer: data.returnOdeMeter,
            calculatedPrice: data.calculatedPrice
        };

        initialized.value = data.returnDateUtc === null;
    } catch (error) {
        console.error("Error fetching booking data:", error);
    } finally {
        loading.value = false;
    }
};

const setReturnDateNow = () => {
    booking.value.returnDate = new Date().toISOString().slice(0, 16);
};

const validateReturnOdometer = () => {
    hasInvalidChars.value = /\D/.test(booking.value.returnOdometer);
};

const submitting = ref(false);

const submitReturn = async () => {
    if (!booking.value.returnDate || !booking.value.returnOdometer) return; // Ensure fields are filled
    submitting.value = true;

    try {
        const response = await fetch(`http://localhost:5270/bookings/${bookingNumber.value}/return`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                date: booking.value.returnDate,
                odometer: booking.value.returnOdometer
            })
        });

        await initializeReturn();
    } catch (error) {
        console.error("Error submitting return:", error);
        alert("Failed to process return.");
    } finally {
        submitting.value = false;
    }
};

const clearForm = () => {
    bookingNumber.value = "";
    booking.value = {
        registrationNumber: "",
        customerIdentification: "",
        date: "",
        odometer: "",
        returnDate: "",
        returnOdometer: ""
    };
    initialized.value = false;
};
</script>