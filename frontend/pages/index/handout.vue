<template>
    <div class="p-6 max-w-lg mx-auto">
        <h1 class="text-2xl font-semibold mb-4">Create Booking</h1>

        <div class="mb-4" :hidden="initialized">
            <UButton :disabled="initialized" :loading="loading" @click="initializeBooking">Start booking
            </UButton>
        </div>
        <UCard class="p-4" :hidden="!initialized">
            <!-- Booking Form -->
            <UForm :state="handout" >
                <div class="mb-4">
                    <UInput v-model="booking.bookingNumber" label="Bookingnumber" disabled />
                </div>

                <div class="mb-4 flex gap-4">
                    <!-- Registration Number Input -->
                    <UInput v-model="booking.registrationNumber" label="Registration Number" placeholder="reg nr"
                        class="flex-1" :disabled="!initialized" />

                    <!-- Car Type Dropdown -->
                    <USelect v-model="booking.type" :options="items" :disabled="!initialized" />
                </div>
                <div class="mb-4">
                    <UInput v-model="booking.customerIdentification" label="Customer ID" placeholder="customer id"
                        :disabled="!initialized" />
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.date" label="Date" type="datetime-local" :disabled="!initialized" />
                </div>

                <div class="mb-4">
                    <UInput placeholder="Odometer" v-model="booking.odometer" label="Odometer" :disabled="!initialized">

                        <template #trailing>
                            <span class="text-gray-500 dark:text-gray-400 text-xs">KM</span>
                        </template>

                    </UInput>
                </div>
            </UForm>

            <div class="mb-4">
                <UButton :loading="loading" @click="createBooking">Create Booking</UButton>
            </div>
        </UCard>
    </div>
</template>

<script setup>
import { ref, computed } from "vue";

const initialized = ref(false);
const items = ["SmallCar", "StationWagon", "Truck"];
const router = useRouter();
const handout = ref({});

const booking = ref({
    bookingNumber: "",
    registrationNumber: "",
    customerIdentification: "",
    type: "",
    date: "",
    odometer: ""
});

const hasInvalidChars = ref(false);

const validateOdometer = computed(() => {
    hasInvalidChars.value = /\D/.test(booking.value.odometer); // Check if there's any non-numeric character
});

const loading = ref(false);

const createBooking = async () => {
    loading.value = true;

    try {
        const response = await fetch("http://localhost:5270/bookings/" + booking.value.bookingNumber, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(booking.value)
        });

        router.push("/handout");
    } catch (error) {
        console.error("Error creating booking:", error);
    } finally {
        loading.value = false;
    }
}

const initializeBooking = async () => {
    loading.value = true;

    try {
        const response = await fetch("http://localhost:5270/bookings/init", { method: "POST" });
        const data = await response.json();
        console.log("Data:", data);
        // Populate form fields
        booking.value = {
            bookingNumber: data.bookingNumber,
        };
        console.log("Booking initialized:", booking.value);
        initialized.value = true;
    } catch (error) {
        console.error("Error initializing booking:", error);
    } finally {
        loading.value = false;
    }
};
</script>